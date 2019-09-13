using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Enums;
using Exemplo.Models.Booking;

namespace Exemplo.Models.Cinema
{
    public class Session
    {
        private Option3D option3D;

        public Theather Theather { get; }
        public Film Film { get; protected set; }
        public Room Room { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public LocalizationOption Localization { get; set; }
        public TimeSpan TrailerTime { get; set; }
        public double? PricingRatio { get; set; }
        public IEnumerable<Reservation> Reservations { get; protected set; }
        public DateTime EndDate => StartDate.Add(this.Film.RunningTime).Add(TrailerTime);
        public Option3D Session3D
        {
            get => option3D;
            set
            {
                if (option3D == Option3D.With3D && Room.Supports3D == Option3D.None)
                {
                    throw new ApplicationException("The session room does not support the supplied 3D method.");
                }

                if (option3D == Option3D.With3D && Film.Film3D == Option3D.None)
                {
                    throw new ApplicationException("The selected film does not support the supplied 3D method.");
                }

                this.option3D = value;
            }
        }

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