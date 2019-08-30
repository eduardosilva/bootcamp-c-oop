using System;

namespace Exemplo.Models.Booking.Pricing
{
    internal class PriceRangeDate : PriceRange
    {
        public DateTime Date { get; }

        public PriceRangeDate(
            DateTime date,
            TimeSpan startTime,
            TimeSpan endTime,
            decimal value
        ) : base(startTime, endTime, value)
        {
            this.Date = date;
        }

        public override decimal GetPrice()
        {
            return this.Value;
        }
    }
}