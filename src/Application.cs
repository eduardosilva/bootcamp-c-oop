using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Enums;
using Exemplo.Models.Cinema;
using Exemplo.Models.Common;
using Exemplo.Queries;

namespace Exemplo
{
    public partial class Application
    {
        private ICollection<Film> films;
        private ICollection<Theather> theathers;

        public IEnumerable<Theather> Theathers => theathers.AsEnumerable();
        public IEnumerable<Film> Films => films.AsEnumerable();

        private Application() {
            this.theathers = new List<Theather>();
            this.films = new List<Film>();
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

        public IEnumerable<SessionsAggregator> ListAllSessions()
        {
            return this.Theathers.Select(t => new SessionsAggregator {
                TheatherName = t.Name,
                Sessions = t.Sessions
            });
        }
    }
}