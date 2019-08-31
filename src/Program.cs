
using System;
using Exemplo.Extensions;
using Exemplo.Models.Booking.Pricing;

namespace Exemplo
{
    class Program
    {
        static void Main(string[] args)
        {
            var sample = Sample.CreateSample();
            sample.Run();

            Console.WriteLine("Execution finished.");
        }
        
    }
}
