using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Enums;
using Exemplo.Models.Booking;
using Exemplo.Models.Cinema;
using Exemplo.Queries;

namespace Exemplo
{
    public partial class Application
    {
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

        private Application() { }

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

    }
}