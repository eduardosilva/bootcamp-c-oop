using System;
using System.Collections.Generic;
using System.Linq;

namespace Exemplo.Models.Booking.Pricing
{
    public class WeekdayPricingTable : IPricingTable<PriceRangeWeekday>
    {
        protected readonly TimeSpan dayStart = new TimeSpan(0, 0, 0);
        protected readonly TimeSpan dayEnd = new TimeSpan(TimeSpan.TicksPerDay);

        protected ICollection<PriceRangeWeekday> priceRules = new List<PriceRangeWeekday>();

        public void AddPricingRule(DayOfWeek weekday, decimal price)
        {
            this.AddPricingRule(weekday, dayStart, dayEnd, price);
        }

        public void AddPricingRule(DayOfWeek weekday, TimeSpan startTime, TimeSpan endTime, decimal price)
        {
            var range = new PriceRangeWeekday(weekday, startTime, endTime, price);
            priceRules.Add(range);
        }

        public IEnumerable<PriceRangeWeekday> GetPricingRules()
        {
            return this.priceRules.AsEnumerable();
        }

        public void RemoveRule(PriceRangeWeekday rule)
        {
            this.priceRules.Remove(rule);
        }

        public decimal GetPrice(DateTime date)
        {
            var dateOnly = date.Date;

            var rule = this.priceRules.Where(r => r.Weekday == date.DayOfWeek)
                .Select(
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