
using System.Collections.Generic;
using Exemplo.Enums;
using Exemplo.Models.Booking;
using Exemplo.Models.Booking.Pricing;
using Exemplo.Models.Cinema;

namespace Exemplo.Services
{
    public interface IBookingService {
        IEnumerable<Ticket> Book(User user, Session session, IEnumerable<Seat> seats);
    }
}
