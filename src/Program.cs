
using System;
using Exemplo.Extensions;
using Exemplo.Models.Booking.Pricing;

namespace Exemplo
{
    class Program
    {
        static void Main(string[] args)
        {
            var wkTable = new WeekdayPricingTable();
            wkTable.AddPricingRule(DayOfWeek.Friday, 100);
            wkTable.AddPricingRule(DayOfWeek.Friday, new TimeSpan(1, 0, 0), new TimeSpan(2, 0, 0), 2000);

            var dateTable = new DatePricingTable();
            dateTable.AddPricingRule(DateTime.Now, 111);

            var value1 = wkTable.TryGetPrice(DateTime.Now);
            var value2 = dateTable.TryGetPrice(DateTime.Now);

            Console.WriteLine(value1);
            Console.WriteLine(value2);
        }
        
    }
}
