
using System;
using System.Linq;
using Exemplo.Enums;
using Exemplo.Extensions;
using Exemplo.Models.Booking.Pricing;
using Exemplo.Models.Cinema;

namespace Exemplo
{
    class Program
    {
        static void Main(string[] args)
        {
            var sample = Application.Startup();

            PrintAllCatalog(sample);
            BookAFilm(sample);
        }

        static void PrintAllCatalog(Application application)
        {
            var sessions = application.ListAllSessions().Select(a => a.Sessions.Select(s =>
               $"{s.Film.Title} - {Enum.GetName(typeof(LocalizationOption), s.Localization)} - "
               + $"{s.StartDate:dd/MM/yyyy HH:mm:ss} - {a.TheatherName} - Room {s.Room.Number}"
               + $"{(s.Session3D == Option3D.With3D ? " [3D]" : "")}"
           )).SelectMany(s => s);

            foreach (var item in sessions)
            {
                Console.WriteLine(item);
            }
        }

        static void BookAFilm(Application application)
        {
            var user = application.Users.First();

            var session = application.ListAllSessions()
                .Where(s => s.TheatherName == "Cinema Pátio Paulista")
                .SelectMany(s => s.Sessions)
                .First(s => s.Film.Title == "Lion King");

            var seats = new (Seat, string)[] {
                (session.Room['F', 7], "João"),
                (session.Room['F', 8], "Maria")
            };           

            application.BookSession(user, session, seats);
            application.BookSession(user, session, seats);

        }
    }
}
