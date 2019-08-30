using System;

namespace Exemplo.Models.Booking.Pricing
{
    internal class PriceRangeWeekday : PriceRange
    {
        public int Weekday { get; }

        public PriceRangeWeekday(
            int weekday,
            TimeSpan startTime,
            TimeSpan endTime,
            decimal value
        ) : base(startTime, endTime, value)
        {            
            this.Weekday = weekday;
        }

        public override decimal GetPrice()
        {
            return this.Value;
        }
    }
}