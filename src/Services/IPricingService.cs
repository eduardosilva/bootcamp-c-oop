
using System;
using System.Collections.Generic;
using Exemplo.Enums;
using Exemplo.Models.Booking.Pricing;
using Exemplo.Services.Logger;

namespace Exemplo.Services
{
    public interface IPricingService : ILog
    {
        decimal GetPrice(DateTime date, Option3D option3D);
    }
}
