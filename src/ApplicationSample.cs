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
        public static Application Startup()
        {
            var instance = new Application();

            SetupTheathers(instance);
            SetupFilms(instance);
            SetupSessions(instance);

            return instance;
        }

        private static void SetupTheathers(Application instance)
        {
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
        }

        private static void SetupFilms(Application instance)
        {
            instance.AddFilm(new Film {
                Title = "Lion King",
                ReleaseDate = new DateTime(2019, 7, 18),
                Genres = new Genre[] { Genre.Animation, Genre.Adventure, Genre.Drama },
                RunningTime = new TimeSpan(1, 58, 0),
                MinimumAge = 10,
                Available3D = true
            });
            instance.AddFilm(new Film {
                Title = "Avengers: Age of Utron",
                ReleaseDate = new DateTime(2015, 4, 23),
                Genres = new Genre[] { Genre.Action, Genre.Adventure, Genre.SciFi },
                RunningTime = new TimeSpan(2, 21, 0),
                MinimumAge = 12,
                Available3D = true
            });
            instance.AddFilm(new Film {
                Title = "Inglorious Basterds",
                ReleaseDate = new DateTime(2009, 10, 9),
                Genres = new Genre[] { Genre.Adventure, Genre.Drama, Genre.War },
                RunningTime = new TimeSpan(2, 33, 0),
                MinimumAge = 18,
                Available3D = false
            });
            instance.AddFilm(new Film {
                Title = "Elite Squad",
                ReleaseDate = new DateTime(2007, 10, 12),
                Genres = new Genre[] { Genre.Action, Genre.Crime, Genre.Drama },
                RunningTime = new TimeSpan(1, 55, 0),
                MinimumAge = 16,
                Available3D = false
            });
            instance.AddFilm(new Film {
                Title = "The Matrix",
                ReleaseDate = new DateTime(1999, 5, 21),
                Genres = new Genre[] { Genre.Action, Genre.SciFi },
                RunningTime = new TimeSpan(2, 16, 0),
                MinimumAge = 12,
                Available3D = false
            });
            instance.AddFilm(new Film {
                Title = "The Godfather",
                ReleaseDate = new DateTime(1972, 9, 10),
                Genres = new Genre[] { Genre.Crime, Genre.Drama },
                RunningTime = new TimeSpan(2, 55, 0),
                MinimumAge = 14,
                Available3D = false
            });
        }

        private static void SetupSessions(Application instance)
        {
            var theather = instance.Theathers.First();
            var roomNumber = theather.Rooms.First().Number;
            var startDate = theather.NextAvaiableSession(roomNumber);

            foreach (var item in instance.Films)
            {
                theather.AddSession(item, roomNumber, startDate);
                startDate = theather.NextAvaiableSession(roomNumber);
            }

            var sessionsList = theather.Sessions.ToList();

            sessionsList[0].Localization = LocalizationOption.Dubbed;
            sessionsList[1].Localization = LocalizationOption.Dubbed;
            sessionsList[2].Localization = LocalizationOption.Subtitled;
            sessionsList[3].Localization = LocalizationOption.None;
            sessionsList[4].Localization = LocalizationOption.Subtitled;
            sessionsList[5].Localization = LocalizationOption.Subtitled;
        }
    }
}