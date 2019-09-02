
using System;
using System.Collections.Generic;
using Exemplo.Enums;
using Exemplo.Models.Booking.Pricing;

namespace Exemplo.Services
{
    public interface IPricingService
    {
        decimal GetPrice(DateTime date, Option3D option3D);
    }
}
