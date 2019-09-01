
using System;
using System.Linq;
using Exemplo.Enums;
using Exemplo.Extensions;
using Exemplo.Models.Booking.Pricing;

namespace Exemplo
{
    class Program
    {
        static void Main(string[] args)
        {
            var sample = Application.Startup();

            var sessions = sample.ListAllSessions().Select(a => a.Sessions.Select(s =>
                $"{s.Film.Title} - {Enum.GetName(typeof(LocalizationOption), s.Localization)} - "
                + $"{s.StartDate:dd/MM/yyyy HH:mm:ss} - {a.TheatherName} - Room {s.Room.Number}"
                + $"{(s.Session3D == Option3D.With3D ? " [3D]" : "")}"
            )).SelectMany(s => s);

            foreach (var item in sessions)
            {
                Console.WriteLine(item);
            }
        }
        
    }
}
