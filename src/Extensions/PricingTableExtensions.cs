using System;
using Exemplo.Models.Booking.Pricing;

namespace Exemplo.Extensions
{
    public static class PricingTableExtensions
    {
        public static bool TryGetPrice<T>(this IPricingTable<T> table, DateTime date, out decimal price)
            where T : IPriceRule
        {
            try
            {
                price = table.GetPrice(date);
                return true;
            }
            catch (System.Exception)
            {
                price = 0;
                return false;
            }
        }
    }
}