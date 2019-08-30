using System;

namespace Exemplo.Models.Booking.Pricing
{
    public interface IPricingTable
    {
        decimal GetPrice(DateTime date);
    }
}