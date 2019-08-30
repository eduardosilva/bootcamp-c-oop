using Exemplo.Models.Cinema;

namespace Exemplo.Models.Booking
{
    public class Ticket
    {
        public int Id { get; set; }
        public User Client { get; set;}
        public Session Session { get; set; }
        public int SeatNumber { get; set; }
    }
}