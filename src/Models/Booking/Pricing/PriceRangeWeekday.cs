using System;

namespace Exemplo.Models.Booking.Pricing
{
    public class PriceRangeWeekday : PriceRange
    {
        public DayOfWeek Weekday { get; }

        public PriceRangeWeekday(
            DayOfWeek weekday,
            TimeSpan startTime,
            TimeSpan endTime,
            decimal value
        ) : base(startTime, endTime, value)
        {            
            this.Weekday = weekday;
        }
    }
}