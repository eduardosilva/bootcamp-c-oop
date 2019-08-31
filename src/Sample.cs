using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Enums;
using Exemplo.Models.Cinema;
using Exemplo.Models.Common;

namespace Exemplo
{
    public class Sample
    {
        private ICollection<Theather> theathers = new List<Theather>();

        private Sample() { }

        public static Sample CreateSample()
        {
            var instance = new Sample();

            instance.AddTheather(t => {
                t.Name = "Cinema Pátio Paulista";
                t.OpeningTime = new TimeSpan(11, 0, 0);
                t.ClosingTime = new TimeSpan(22, 0, 0);
                t.Address = new Location {
                    Country = "Brazil",
                    State = "São Paulo",
                    City = "São Paulo",
                    Address = "Rua Treze de Maio, 1947",
                    Apartment = "Arco N 501"
                };

                t.AddRoom(new Room {
                    Number = 1,
                    ScreenType = ScreenType.IMAX,
                    Supports3D = true
                }, (builder) => {
                    builder.HasRow('A', (row) => {
                        row.HasSeatRange(1, 14);
                    });
                    builder.HasRow('B', (row) => {
                        row.HasSeatRange(1, 11);
                        row.WithSeatAt(2, (seat) => seat.Type = SeatType.Companion);
                        row.WithSeatAt(3, (seat) => seat.Type = SeatType.Handicap);
                        row.WithSeatAt(4, (seat) => seat.Type = SeatType.Handicap);
                        row.WithSeatAt(5, (seat) => seat.Type = SeatType.Companion);
                        row.WithSeatAt(7, (seat) => seat.Type = SeatType.Companion);
                        row.WithSeatAt(8, (seat) => seat.Type = SeatType.Handicap);
                        row.WithSeatAt(9, (seat) => seat.Type = SeatType.Handicap);
                        row.WithSeatAt(10, (seat) => seat.Type = SeatType.Companion);
                    });
                    builder.HasRow('C', (row) => {
                        row.HasSeatRange(1, 14);
                        row.WithSeatAt(1, (seat) => seat.Type = SeatType.ReducedMobility);
                        row.WithSeatAt(2, (seat) => seat.Type = SeatType.Companion);
                        row.WithSeatAt(13, (seat) => seat.Type = SeatType.Companion);
                        row.WithSeatAt(14, (seat) => seat.Type = SeatType.ReducedMobility);
                    });
                    builder.HasRow('D', (row) => {
                        row.HasSeatRange(1, 14);
                        row.WithSeatAt(1, (seat) => seat.Type = SeatType.ReducedMobility);
                        row.WithSeatAt(2, (seat) => seat.Type = SeatType.Companion);
                        row.WithSeatAt(13, (seat) => seat.Type = SeatType.Companion);
                        row.WithSeatAt(14, (seat) => seat.Type = SeatType.ReducedMobility);
                    });
                    builder.HasRow('E', (row) => {
                        row.HasSeatRange(1, 14);
                    });
                    builder.HasRow('F', (row) => {
                        row.HasSeatRange(1, 17);
                        row.WithSeatAt(15, (seat) => seat.Type = SeatType.Obese);
                        row.WithSeatAt(16, (seat) => seat.Type = SeatType.Obese);
                        row.WithSeatAt(17, (seat) => seat.Type = SeatType.Companion);
                    });
                    builder.HasRow('G', (row) => {
                        row.HasSeatRange(1, 17);
                        row.WithSeatAt(15, (seat) => seat.Type = SeatType.Obese);
                        row.WithSeatAt(16, (seat) => seat.Type = SeatType.Obese);
                        row.WithSeatAt(17, (seat) => seat.Type = SeatType.Companion);
                    });
                    builder.HasRow('H', (row) => {
                        row.HasSeatRange(1, 17);
                        row.WithSeatAt(15, (seat) => seat.Type = SeatType.Obese);
                        row.WithSeatAt(16, (seat) => seat.Type = SeatType.Obese);
                        row.WithSeatAt(17, (seat) => seat.Type = SeatType.Companion);
                    });
                    builder.HasRow('I', (row) => {
                        row.HasSeatRange(1, 17);
                    });
                    builder.HasRow('J', (row) => {
                        row.HasSeatRange(1, 17);
                    });
                    builder.HasRow('K', (row) => {
                        row.HasSeatRange(1, 17);
                    });
                    builder.HasRow('L', (row) => {
                        row.HasSeatRange(1, 17);
                    });
                    builder.HasRow('L', (row) => {
                        row.HasSeatRange(1, 22);
                    });
                });

            });

            return instance;
        }

        public void Run()
        {
            Console.WriteLine(this.theathers.First().Rooms.First().Capacity);
        }

        private void AddTheather(Action<Theather> builder)
        {
            var theather = new Theather();

            builder(theather);
            this.theathers.Add(theather);
        }
    }
}