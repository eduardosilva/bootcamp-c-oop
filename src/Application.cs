using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Enums;
using Exemplo.Models.Booking;
using Exemplo.Models.Cinema;
using Exemplo.Queries;
using Exemplo.Services;

namespace Exemplo
{
    public partial class Application
    {
        private IBookingService bookingService;
        private IPricingService pricingService;


        private ICollection<Film> films = new List<Film>();
        private ICollection<Theather> theathers = new List<Theather>();
        private ICollection<User> users = new List<User>();

        public IEnumerable<Theather> Theathers => theathers.AsEnumerable();
        public IEnumerable<Film> Films => films.AsEnumerable();
        public IEnumerable<User> Users => users.AsEnumerable();

        public IEnumerable<SessionsAggregator> ListAllSessions()
        {
            return this.Theathers.Select(t => new SessionsAggregator {
                TheatherName = t.Name,
                Sessions = t.Sessions
            });
        }

        public void AddTheather(Action<Theather> builder)
        {
            var theather = new Theather();

            builder(theather);
            this.theathers.Add(theather);
        }

        public void AddFilm(Film film)
        {
            this.films.Add(film);
        }

        public IEnumerable<Ticket> BookSession(User user, Session session, IEnumerable<(Seat, string)> seats)
        {
            var pricingService = new PricingService(session.Theather.PriceProviders);
            var bookingService = new BookingService(pricingService);

            return bookingService.Book(user, session, seats);
        }

    }
}