
using System;
using System.Collections.Generic;
using Exemplo.Enums;
using Exemplo.Models.Booking;
using Exemplo.Models.Booking.Pricing;
using Exemplo.Models.Cinema;

namespace Exemplo.Services
{
    public interface IBookingService {
        Action<string> Logger { get; }
        IEnumerable<Ticket> Book(User user, Session session, IEnumerable<(Seat, string)> seats);
    }
}
