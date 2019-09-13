
using System;
using System.Collections.Generic;
using Exemplo.Enums;
using Exemplo.Models.Booking.Pricing;

namespace Exemplo.Services
{
    public interface IPricingService
    {
        Action<string> Logger { get; }
        decimal GetPrice(DateTime date, Option3D option3D);
    }
}
