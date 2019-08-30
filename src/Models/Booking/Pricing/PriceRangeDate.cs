using System;

namespace Exemplo.Models.Booking.Pricing
{
    public class PriceRangeDate : PriceRange
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
    }
}