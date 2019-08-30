using System;

namespace Exemplo.Models.Booking.Pricing
{
    public class PriceRange : IPriceRange
    {
        public int Weekday { get => weekday; }


        public PriceRangeWeekday(
            int weekday,
            TimeSpan startTime,
            TimeSpan endTime
        )
        {
            this.weekday = weekday;
            this.price = price;
        }

        public decimal GetPrice()
        {
            return price;
        }
    }
}