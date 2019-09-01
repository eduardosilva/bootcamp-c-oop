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
        public bool Supports3D { get; set; }

        public IEnumerable<Seat> AllSeats => this.Rows.SelectMany(r => r.Seats);
        public int Capacity => this.AllSeats.Count();

        public Seat this[string seat]
        {
            get {
                var row = seat.Substring(0, 1);
                var numberString = seat.Substring(1, seat.Length);

                int number;

                if (row.Length != 1 || Int32.TryParse(numberString, out number))
                {
                    throw new ApplicationException($"Invalid seat code {seat}");
                }

                return this[row[0], number];
            }            
        }

        public Seat this[char row, int number]
        {
            get {
                return this.Rows.Where(r => r.Letter == row)
                    .SelectMany(r => r.Seats)
                    .Single(s => s.Number == number);
            }
        }

        public Room Clone(int newRoomNumber)
        {
            var newRoom = new Room {
                Number = newRoomNumber,
                ScreenType = this.ScreenType,
                Supports3D = this.Supports3D
            };

            newRoom.Rows = this.Rows.Select(r => new Row {
                Letter = r.Letter,
                Seats = r.Seats.Select(s => new Seat {
                    Number = s.Number,
                    Type = s.Type
                }).ToList()
            }).ToList();

            return newRoom;
        }
    }
    
}