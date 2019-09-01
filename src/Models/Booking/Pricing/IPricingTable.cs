using System;
using System.Collections.Generic;

namespace Exemplo.Models.Booking.Pricing
{
    public interface IPricingTable<T> : IPriceProvider
        where T : IPriceRule
    {
        void RemoveRule(T rule);
        IEnumerable<T> GetPricingRules();
    }
}