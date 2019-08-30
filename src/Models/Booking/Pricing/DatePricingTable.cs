using System;
using System.Collections.Generic;
using System.Linq;

namespace Exemplo.Models.Booking.Pricing
{
    public class DatePricingTable : IPricingTable<PriceRangeDate>
    {
        protected readonly TimeSpan dayStart = new TimeSpan(0, 0, 0);
        protected readonly TimeSpan dayEnd = new TimeSpan(TimeSpan.TicksPerDay);

        protected ICollection<PriceRangeDate> priceRules = new List<PriceRangeDate>();

        public void AddPricingRule(DateTime date, decimal price)
        {
            this.AddPricingRule(date, dayStart, dayEnd, price);
        }

        public void AddPricingRule(DateTime date, TimeSpan startTime, TimeSpan endTime, decimal price)
        {
            var range = new PriceRangeDate(date, startTime, endTime, price);
            priceRules.Add(range);
        }

        public IEnumerable<PriceRangeDate> GetPricingRules()
        {
            return this.priceRules.AsEnumerable();
        }

        public void RemoveRule(PriceRangeDate rule)
        {
            this.priceRules.Remove(rule);
        }

        public decimal GetPrice(DateTime date)
        {
            var dateOnly = date.Date;

            var rule = this.priceRules
                .Where(r => r.Date.Date == dateOnly)
                .Select(
                    r => new
                    {
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