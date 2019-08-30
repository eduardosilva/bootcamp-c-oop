using System;
using System.Collections.Generic;
using System.Linq;

namespace Exemplo.Models.Booking.Pricing
{
    public class DateRangePricingTable : IPricingTable
    {
        protected readonly TimeSpan dayStart = new TimeSpan(0, 0, 0);
        protected readonly TimeSpan dayEnd = new TimeSpan(TimeSpan.TicksPerDay);

        protected ICollection<PriceRange> priceRules = new List<PriceRange>();

        public void AddPricingRule(int weekday, decimal price)
        {
            this.AddPricingRule(weekday, dayStart, dayEnd, price);
        }

        public void AddPricingRule(int weekday, TimeSpan startTime, TimeSpan endTime, decimal price)
        {
            var range = new PriceRangeWeekday(weekday, startTime, endTime, price);
            priceRules.Add(range);
        }

        public void AddPricingRule(DateTime date, decimal price)
        {
            this.AddPricingRule(date, dayStart, dayEnd, price);
        }

        public void AddPricingRule(DateTime date, TimeSpan startTime, TimeSpan endTime, decimal price)
        {
            var range = new PriceRangeDate(date, startTime, endTime, price);
            priceRules.Add(range);
        }

        public decimal GetPrice(DateTime date)
        {
            var dateOnly = date.Date;

            IEnumerable<PriceRange> foundPriceRules = this.priceRules.Where(
                r => r.GetType() == typeof(PriceRangeDate)
            )
                .Cast<PriceRangeDate>()
                .Where(r => r.Date.Date == dateOnly);

            if (foundPriceRules.Count() == 0)
            {
                foundPriceRules = this.priceRules.Where(
                    r => r.GetType() == typeof(PriceRangeWeekday)
                )
                    .Cast<PriceRangeWeekday>()
                    .Where(r => r.Weekday == (int)date.DayOfWeek);
            }

            var rule = foundPriceRules.Select(
                r => new {
                    r.Value,
                    StartDate = dateOnly.Add(r.StartTime),
                    EndDate = dateOnly.Add(r.EndTime)
                }
            )
                .Where(r => date >= r.StartDate && date <= r.EndDate)
                .OrderBy(r => r.EndDate.Ticks - r.StartDate.Ticks)
                .FirstOrDefault();

            if (rule == null)
            {
                throw new Exception("No price rule found for specified date.");
            }

            return rule.Value;
        }
    }
}