using System;
using System.Collections.Generic;

namespace Exemplo.Models.Booking.Pricing
{
    public interface IPriceProvider
    {
        decimal GetPrice(DateTime date);
    }
}