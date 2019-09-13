using System;
using Exemplo.Models.Cinema;

namespace Exemplo.Models.Booking
{
    public class Reservation
    {
        public Seat Seat { get; }
        public User Reserver { get; set; }
        public Ticket ReservationTicket { get; set; }
        public bool IsReserved { get; set; }
        public bool IsAvaiable { get; set; }
        public DateTime? ReservationDate { get; set; }

        public Reservation(Seat seat)
        {
            this.Seat = seat;
            this.IsAvaiable = true;
        }
    }
}