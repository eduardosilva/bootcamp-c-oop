using System;
using System.Collections.Generic;

namespace Exemplo.Models.Cinema
{
    public class Threather {
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
    }
}