using System;

namespace Exemplo.Models.Booking.Pricing
{
    public interface IPricingTable<T>
        where T: IPriceRule
    {
        decimal GetPrice(DateTime date);
    }
}