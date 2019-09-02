using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Enums;

namespace Exemplo.Models.Cinema
{
    public class Room
    {
        public int Number { get; set; }
        public ScreenType ScreenType { get; set; }
        public IEnumerable<Row> Rows { get; internal set; }
        public Option3D Supports3D { get; set; }

        public IEnumerable<Seat> AllSeats => this.Rows.SelectMany(r => r.Seats);
        public int Capacity => this.AllSeats.Count();

        public Seat this[string seat]
        {
            get
            {
               return this.AllSeats.Single(s => s.Name == seat);
            }
        }

        public Seat this[char row, int number]
        {
            get
            {
                return this.Rows.Where(r => r.Letter == row)
                    .SelectMany(r => r.Seats)
                    .Single(s => s.Number == number);
            }
        }

        public Room Clone(int newRoomNumber)
        {
            var newRoom = new Room
            {
                Number = newRoomNumber,
                ScreenType = this.ScreenType,
                Supports3D = this.Supports3D
            };

            newRoom.Rows = this.Rows.Select(r =>
            {
                var row = new Row
                {
                    Letter = r.Letter
                };

                row.Seats = r.Seats.Select(s => new Seat(row)
                {
                    Number = s.Number,
                    Type = s.Type
                }).ToList();

                return row;

            }).ToList();

            return newRoom;
        }
    }

}