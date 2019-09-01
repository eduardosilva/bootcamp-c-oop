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

        public BookingService(
            IPricingService pricingService
        )
        {
            this.pricingService = pricingService;
        }

        public IEnumerable<Ticket> Book(User user, Session session, IEnumerable<(Seat, string)> seats)
        {
            var reservedSeats = (
                from s in seats
                join r in session.Reservations on s.Item1 equals r.Seat
                select new { Reservation = r, Name = s.Item2 }
            );
                        

            if (reservedSeats.Count() != seats.Count())
            {
                throw new ApplicationException("One or more selected seats do not belong to this session.");
            }

            if (reservedSeats.Any(r => !r.Reservation.IsAvaiable))
            {
                throw new ApplicationException("One or more selected seats is not available for selection.");
            }

            if (reservedSeats.Any(r => r.Reservation.IsReserved))
            {
                throw new ApplicationException("One or more selected seats is already reserved.");
            }

            decimal price = pricingService.GetPrice(session.StartDate);

            if (session.PricingRatio != null)
            {
                price = Convert.ToDecimal(Convert.ToDouble(price) * session.PricingRatio.Value);
            }

            var total = price * reservedSeats.Count();

            // call payment service here

            var tickets = new List<Ticket>();

            foreach (var item in reservedSeats)
            {
                var ticket = new Ticket {
                    Id = Guid.NewGuid(),
                    ReservationName = item.Name,
                    SeatName = item.Reservation.Seat.Name,
                    Session = session
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
