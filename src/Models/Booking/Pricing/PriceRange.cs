using System;

namespace Exemplo.Models.Booking.Pricing
{
    public abstract class PriceRange: IPriceRule, ITimeRange
    {
        public virtual TimeSpan StartTime { get; protected set; }
        public virtual TimeSpan EndTime { get; protected set; }
        public virtual decimal Value { get; protected set; }

        public PriceRange(TimeSpan startTime, TimeSpan endTime, decimal value)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Value = value;
        }

        public abstract decimal GetPrice();
    }
}