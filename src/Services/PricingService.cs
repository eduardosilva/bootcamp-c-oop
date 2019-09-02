
using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Enums;
using Exemplo.Models.Booking.Pricing;

namespace Exemplo.Services
{
    public class PricingService : IPricingService
    {
        private IDictionary<Option3D, IEnumerable<IPriceProvider>> pricingPriorityList;

        public PricingService(
            IDictionary<Option3D, IEnumerable<IPriceProvider>> pricingPriorityList
        )
        {
            this.pricingPriorityList = pricingPriorityList;
        }

        public decimal GetPrice(DateTime date, Option3D option3D = Option3D.None)
        {
            var providers = pricingPriorityList[option3D];
            
            foreach (var item in providers)
            {
                try
                {
                    return item.GetPrice(date);
                }
                catch (System.Exception) { }
            }

            throw new ApplicationException("No pricing policy defined for the period.");
        }
    }
}
