using System;
using Exemplo.Models.Cinema;

namespace Exemplo.Models.Booking
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string FilmTitle { get; set; }
        public DateTime StartDate { get; set; }
        public string Room { get; set; }
        public string ReservationName { get; set; }
        public string SeatName { get; set; }
    }
}