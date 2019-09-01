using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Extensions;
using Exemplo.Models.Common;

namespace Exemplo.Models.Cinema
{
    public class Theather
    {
        protected readonly int roundingMinute = 30;

        protected ICollection<Room> rooms = new List<Room>();
        protected ICollection<Session> sessions = new List<Session>();

        public string Name { get; set; }
        public Location Address { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public IEnumerable<Room> Rooms => this.rooms.AsEnumerable();
        public IEnumerable<Session> Sessions => this.sessions.AsEnumerable();

        public void AddRoom(Room room, Action<RoomLayoutBuilder> builder)
        {
            var layoutBuilder = new RoomLayoutBuilder();
            builder(layoutBuilder);

            room.Rows = layoutBuilder.Build();

            this.rooms.Add(room);
        }

        public DateTime NextAvaiableSession(int roomNumber)
        {
            return this.NextAvaiableSession(this.Rooms.First(r => r.Number == roomNumber));
        }

        public DateTime NextAvaiableSession(Room room)
        {
            DateTime minDate;
            var openingCurrent = DateTime.Today.Add(this.OpeningTime);
            var closingCurrent = DateTime.Today.Add(this.ClosingTime);            

            var sessionsForRoom = this.Sessions.Where(s => s.Room == room);

            var lastSession = sessionsForRoom.OrderByDescending(s => s.EndDate).FirstOrDefault();

            if (lastSession == null)
            {
                minDate = DateTime.Now;
            }
            else
            {
                openingCurrent = lastSession.StartDate.Date.Add(this.OpeningTime);
                closingCurrent = lastSession.StartDate.Date.Add(this.ClosingTime);
                minDate = lastSession.EndDate;
            }

            minDate = minDate.RoundToNext(roundingMinute);

            if (minDate <= openingCurrent)
            {
                minDate = openingCurrent;
            }

            if (minDate >= closingCurrent)
            {
                minDate = openingCurrent.AddDays(1);
            }

            return minDate.RoundToNext(roundingMinute);
        }

        public Session AddSession(Film film, int roomNumber, DateTime startDate)
        {
            var room = this.Rooms.Where(r => r.Number == roomNumber).First();
            var minDate = this.NextAvaiableSession(roomNumber);

            if (startDate < minDate)
            {
                throw new ApplicationException($"Invalid session time for this room. The earliest available date is {minDate}");
            }

            var session = new Session(film, room, startDate);

            this.sessions.Add(session);
            return session;
        }
    }
}