using System;
using Exemplo.Models.Booking.Pricing;

namespace Exemplo.Extensions
{
    public static class PricingTableExtensions
    {
        public static decimal? TryGetPrice<T>(this IPricingTable<T> table, DateTime date)
            where T : IPriceRule
        {
            try
            {
                return table.GetPrice(date);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}