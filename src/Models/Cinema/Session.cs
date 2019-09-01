using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Enums;
using Exemplo.Models.Booking;

namespace Exemplo.Models.Cinema
{
    public class Session
    {
        public Theather Theather { get; }
        public Film Film { get; protected set; }
        public Room Room { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public LocalizationOption Localization { get; set; }
        public Option3D Session3D { get; set; }
        public TimeSpan TrailerTime { get; set; }
        public double? PricingRatio { get; set; }
        public IEnumerable<Reservation> Reservations { get; protected set; }

        public DateTime EndDate => StartDate.Add(this.Film.RunningTime).Add(TrailerTime);

        internal Session(Theather theather, Film film, Room room, DateTime startDate)
        {
            this.Theather = theather;
            this.Film = film;
            this.Room = room;
            this.StartDate = startDate;

            this.Reservations = room.AllSeats.Select(s => new Reservation(s)).ToList();

            this.TrailerTime = new TimeSpan(0);
        }
    }
}