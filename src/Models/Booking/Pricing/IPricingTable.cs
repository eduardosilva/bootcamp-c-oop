using System;
using System.Collections.Generic;

namespace Exemplo.Models.Booking.Pricing
{
    public interface IPricingTable<T>
        where T : IPriceRule
    {
        void RemoveRule(T rule);

        decimal GetPrice(DateTime date);
        IEnumerable<T> GetPricingRules();
    }
}