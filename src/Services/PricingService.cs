
using System.Collections.Generic;
using Exemplo.Enums;
using Exemplo.Models.Booking.Pricing;

namespace Exemplo.Services
{
    public class PricingService {

        public PricingService(
            IDictionary<Option3D, IEnumerable<IPriceProvider>> pricingPriorityList
        )
        {

        }

    }
}
