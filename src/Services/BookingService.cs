using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Models.Booking;
using Exemplo.Models.Cinema;

namespace Exemplo.Services
{
    public class BookingService : IBookingService
    {
        private IPricingService pricingService;
        private IPaymentService paymentService;

        public Action<string> Logger { get; set; }

        public BookingService(
            IPricingService pricingService,
            IPaymentService paymentService
        )
        {
            this.pricingService = pricingService;
            this.paymentService = paymentService;
        }

        public IEnumerable<Ticket> Book(User user, Session session, IEnumerable<(Seat, string)> seats)
        {
            if (seats.Select(s => s.Item1).Distinct().Count() != seats.Count())
            {
                throw new ApplicationException("One or more seats were selected multiple times.");
            }
                        
            var reservedSeats = (
                from s in seats
                join r in session.Reservations on s.Item1 equals r.Seat
                select new { Reservation = r, Name = s.Item2 }
            ).ToList();

            if (reservedSeats.Count != seats.Count())
            {
                throw new ApplicationException("One or more selected seats do not belong to this session.");
            }

            if (reservedSeats.Any(r => !r.Reservation.IsAvaiable))
            {
                throw new ApplicationException("One or more selected seats are not available for selection.");
            }

            if (reservedSeats.Any(r => r.Reservation.IsReserved))
            {
                throw new ApplicationException("One or more selected seats are already reserved.");
            }

            decimal price = pricingService.GetPrice(session.StartDate, session.Session3D);

            if (session.PricingRatio != null)
            {
                price = Convert.ToDecimal(Convert.ToDouble(price) * session.PricingRatio.Value);
            }

            var total = price * reservedSeats.Count();

            if (!paymentService.ExecutePayment(user.Document, total).Result)
            {
                throw new ApplicationException("Payment refused.");
            }

            var tickets = new List<Ticket>();

            foreach (var item in reservedSeats)
            {
                var ticket = new Ticket {
                    Id = Guid.NewGuid(),
                    FilmTitle = session.Film.Title,
                    StartDate = session.StartDate,
                    Room = $"Room {session.Room.Number}",
                    ReservationName = item.Name,
                    SeatName = item.Reservation.Seat.Name,
                };

                item.Reservation.IsReserved = true;
                item.Reservation.ReservationDate = DateTime.Now;
                item.Reservation.Reserver = user;
                item.Reservation.ReservationTicket = ticket;

                tickets.Add(ticket);
            }

            return tickets;
        }

    }
}
