using System;

namespace Exemplo.Models.Booking.Pricing
{
    public interface ITimeRange
    {
        TimeSpan StartTime { get; }
        TimeSpan EndTime { get; }
    }
}