using System;

namespace Exemplo.Models.Booking.Pricing
{
    public class DiscountPriceProvider : IPriceProvider
    {
        private IPriceProvider provider;
        
        public double DiscountPercent { get; set; }

        public DiscountPriceProvider(IPriceProvider provider, double discountPercent)
        {
            this.provider = provider;
            this.DiscountPercent = discountPercent;
        }

        public decimal GetPrice(DateTime date)
        {
            double value = Convert.ToDouble(provider.GetPrice(date));
            
            return Convert.ToDecimal(value - (value * DiscountPercent));
        }
    }
}