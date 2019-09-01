using System;
using Exemplo.Models.Cinema;

namespace Exemplo.Models.Booking
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public Session Session { get; set; }
        public string ReservationName { get; set; }
        public string SeatName { get; set; }
    }
}