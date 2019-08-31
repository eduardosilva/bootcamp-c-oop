using System;
using System.Collections.Generic;
using Exemplo.Models.Common;

namespace Exemplo.Models.Cinema
{
    public class Theather
    {
        protected ICollection<Room> rooms = new List<Room>();

        public string Name { get; set; }
        public Location Address { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public IEnumerable<Room> Rooms => rooms;
        public ICollection<Session> Sessions { get; protected set; }

        public void AddRoom(Room room, Action<RoomLayoutBuilder> builder)
        {
            var layoutBuilder = new RoomLayoutBuilder();
            builder(layoutBuilder);

            room.Rows = layoutBuilder.Build();

            this.rooms.Add(room);
        }
    }
}