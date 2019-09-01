using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Enums;
using Exemplo.Models.Cinema;
using Exemplo.Models.Common;

namespace Exemplo
{
    public partial class Application
    {
        private ICollection<Film> films;
        private ICollection<Theather> theathers;
        private ICollection<Session> sessions;


        public IEnumerable<Theather> Theathers => theathers.AsEnumerable();
        public IEnumerable<Film> Films => films.AsEnumerable();
        public IEnumerable<Session> Sessions => sessions.AsEnumerable();


        private Application() {
            this.theathers = new List<Theather>();
            this.films = new List<Film>();
        }

        public void Run()
        {
            Console.WriteLine(this.Theathers.First().Rooms.First().Capacity);
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

        public void AddSession(Session session)
        {
            if (this.Sessions.Any(s => s.Room == session.Room
                && s.StartDate <= session.EndDate
                && s.EndDate >= session.StartDate))
            {
                throw new ApplicationException("A session is already scheduled for this room in this time.");
            }

            this.sessions.Add(session);
        }

        public Film GetFilm(string title)
        {
            return this.films.FirstOrDefault(f => f.Title == title);
        }
    }
}