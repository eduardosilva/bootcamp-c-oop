using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Enums;
using Exemplo.Models.Booking;
using Exemplo.Models.Cinema;
using Exemplo.Models.Common;

namespace Exemplo
{
    public partial class Application
    {
        public static readonly int nightTime = 17;

        public static Application Startup()
        {
            var instance = new Application();

            SetupTheathers(instance);
            SetupFilms(instance);
            SetupSessions(instance);
            SetupUsers(instance);

            return instance;
        }

        private static void SetupTheathers(Application instance)
        {
            instance.AddTheather(t =>
            {
                t.Name = "Cinema Pátio Paulista";
                t.OpeningTime = new TimeSpan(11, 0, 0);
                t.ClosingTime = new TimeSpan(22, 0, 0);
                t.Address = new Location
                {
                    Country = "Brazil",
                    State = "São Paulo",
                    City = "São Paulo",
                    Address = "Rua Treze de Maio, 1947",
                    Apartment = "Arco N 501"
                };

                SetupPricingScheme(t);
                SetupRoomScheme(t);
            });
        }

        private static void SetupFilms(Application instance)
        {
            instance.AddFilm(new Film
            {
                Title = "Lion King",
                ReleaseDate = new DateTime(2019, 7, 18),
                Genres = new Genre[] { Genre.Animation, Genre.Adventure, Genre.Drama },
                RunningTime = new TimeSpan(1, 58, 0),
                MinimumAge = 10,
                Film3D = Option3D.With3D
            });
            instance.AddFilm(new Film
            {
                Title = "Avengers: Age of Utron",
                ReleaseDate = new DateTime(2015, 4, 23),
                Genres = new Genre[] { Genre.Action, Genre.Adventure, Genre.SciFi },
                RunningTime = new TimeSpan(2, 21, 0),
                MinimumAge = 12,
                Film3D = Option3D.With3D
            });
            instance.AddFilm(new Film
            {
                Title = "Inglorious Basterds",
                ReleaseDate = new DateTime(2009, 10, 9),
                Genres = new Genre[] { Genre.Adventure, Genre.Drama, Genre.War },
                RunningTime = new TimeSpan(2, 33, 0),
                MinimumAge = 18,
                Film3D = Option3D.None
            });
            instance.AddFilm(new Film
            {
                Title = "Elite Squad",
                ReleaseDate = new DateTime(2007, 10, 12),
                Genres = new Genre[] { Genre.Action, Genre.Crime, Genre.Drama },
                RunningTime = new TimeSpan(1, 55, 0),
                MinimumAge = 16,
                Film3D = Option3D.None
            });
            instance.AddFilm(new Film
            {
                Title = "The Matrix",
                ReleaseDate = new DateTime(1999, 5, 21),
                Genres = new Genre[] { Genre.Action, Genre.SciFi },
                RunningTime = new TimeSpan(2, 16, 0),
                MinimumAge = 12,
                Film3D = Option3D.None
            });
            instance.AddFilm(new Film
            {
                Title = "The Godfather",
                ReleaseDate = new DateTime(1972, 9, 10),
                Genres = new Genre[] { Genre.Crime, Genre.Drama },
                RunningTime = new TimeSpan(2, 55, 0),
                MinimumAge = 14,
                Film3D = Option3D.None
            });
        }

        private static void SetupSessions(Application instance)
        {
            var theather = instance.Theathers.First();
            var roomNumber = theather.Rooms.First().Number;
            var startDate = theather.NextAvaiableSession(roomNumber);

            foreach (var item in instance.Films)
            {
                var session = theather.AddSession(item, roomNumber, startDate);

                if (item.Film3D == Option3D.With3D)
                {
                    session.Session3D = Option3D.With3D;
                }

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

        private static void SetupUsers(Application instance)
        {
            instance.users.Add(new User
            {
                Name = "Edu",
                Document = "420.948.240-47",
                BirthDate = new DateTime(1984, 7, 18)
            });
            instance.users.Add(new User
            {
                Name = "Vinicius",
                Document = "889.717.360-89",
                BirthDate = new DateTime(1989, 11, 2)
            });
            instance.users.Add(new User
            {
                Name = "Gabriel",
                Document = "260.531.470-79",
                BirthDate = new DateTime(1996, 3, 27)
            });
        }

        private static void SetupRoomScheme(Theather theather)
        {
            var room = new Room
            {
                Number = 1,
                ScreenType = ScreenType.IMAX,
                Supports3D = true
            };

            theather.AddRoom(room, (builder) =>
            {
                builder.HasRow('A', (row) =>
                {
                    row.HasSeatRange(1, 14);
                });
                builder.HasRow('B', (row) =>
                {
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
                builder.HasRow('C', (row) =>
                {
                    row.HasSeatRange(1, 14);
                    row.WithSeatAt(1, (seat) => seat.Type = SeatType.ReducedMobility);
                    row.WithSeatAt(2, (seat) => seat.Type = SeatType.Companion);
                    row.WithSeatAt(13, (seat) => seat.Type = SeatType.Companion);
                    row.WithSeatAt(14, (seat) => seat.Type = SeatType.ReducedMobility);
                });
                builder.HasRow('D', (row) =>
                {
                    row.HasSeatRange(1, 14);
                    row.WithSeatAt(1, (seat) => seat.Type = SeatType.ReducedMobility);
                    row.WithSeatAt(2, (seat) => seat.Type = SeatType.Companion);
                    row.WithSeatAt(13, (seat) => seat.Type = SeatType.Companion);
                    row.WithSeatAt(14, (seat) => seat.Type = SeatType.ReducedMobility);
                });
                builder.HasRow('E', (row) =>
                {
                    row.HasSeatRange(1, 14);
                });
                builder.HasRow('F', (row) =>
                {
                    row.HasSeatRange(1, 17);
                    row.WithSeatAt(15, (seat) => seat.Type = SeatType.Obese);
                    row.WithSeatAt(16, (seat) => seat.Type = SeatType.Obese);
                    row.WithSeatAt(17, (seat) => seat.Type = SeatType.Companion);
                });
                builder.HasRow('G', (row) =>
                {
                    row.HasSeatRange(1, 17);
                    row.WithSeatAt(15, (seat) => seat.Type = SeatType.Obese);
                    row.WithSeatAt(16, (seat) => seat.Type = SeatType.Obese);
                    row.WithSeatAt(17, (seat) => seat.Type = SeatType.Companion);
                });
                builder.HasRow('H', (row) =>
                {
                    row.HasSeatRange(1, 17);
                    row.WithSeatAt(15, (seat) => seat.Type = SeatType.Obese);
                    row.WithSeatAt(16, (seat) => seat.Type = SeatType.Obese);
                    row.WithSeatAt(17, (seat) => seat.Type = SeatType.Companion);
                });
                builder.HasRow('I', (row) =>
                {
                    row.HasSeatRange(1, 17);
                });
                builder.HasRow('J', (row) =>
                {
                    row.HasSeatRange(1, 17);
                });
                builder.HasRow('K', (row) =>
                {
                    row.HasSeatRange(1, 17);
                });
                builder.HasRow('L', (row) =>
                {
                    row.HasSeatRange(1, 17);
                });
                builder.HasRow('L', (row) =>
                {
                    row.HasSeatRange(1, 22);
                });
            });

            theather.CloneRoom(room, 2);
            theather.CloneRoom(room, 3);
        }

        private static void SetupPricingScheme(Theather theather)
        {
            theather.HasWeekdayPricingScheme(table => {
                var night = TimeSpan.FromHours(nightTime);

                table.AddPricingRule(
                    DayOfWeek.Monday,
                    theather.OpeningTime,
                    night,
                    33
                );

                table.AddPricingRule(
                    DayOfWeek.Monday,
                    night,
                    theather.ClosingTime,
                    35
                );

                table.AddPricingRule(
                    DayOfWeek.Tuesday,
                    theather.OpeningTime,
                    night,
                    33
                );

                table.AddPricingRule(
                    DayOfWeek.Tuesday,
                    night,
                    theather.ClosingTime,
                    35
                );

                table.AddPricingRule(
                    DayOfWeek.Wednesday,
                    theather.OpeningTime,
                    night,
                    32
                );

                table.AddPricingRule(
                    DayOfWeek.Wednesday,
                    night,
                    theather.ClosingTime,
                    33
                );

                table.AddPricingRule(
                    DayOfWeek.Thursday,
                    theather.OpeningTime,
                    night,
                    38
                );

                table.AddPricingRule(
                    DayOfWeek.Thursday,
                    night,
                    theather.ClosingTime,
                    40
                );

                table.AddPricingRule(
                    DayOfWeek.Friday,
                    theather.OpeningTime,
                    night,
                    38
                );

                table.AddPricingRule(
                    DayOfWeek.Friday,
                    night,
                    theather.ClosingTime,
                    40
                );

                table.AddPricingRule(DayOfWeek.Saturday, 40);
                table.AddPricingRule(DayOfWeek.Sunday, 40);
            });

            theather.HasWeekdayPricingScheme(table => {
                var night = TimeSpan.FromHours(nightTime);

                table.AddPricingRule(
                    DayOfWeek.Monday,
                    theather.OpeningTime,
                    night,
                    40
                );

                table.AddPricingRule(
                    DayOfWeek.Monday,
                    night,
                    theather.ClosingTime,
                    41
                );

                table.AddPricingRule(
                    DayOfWeek.Tuesday,
                    theather.OpeningTime,
                    night,
                    40
                );

                table.AddPricingRule(
                    DayOfWeek.Tuesday,
                    night,
                    theather.ClosingTime,
                    41
                );

                table.AddPricingRule(
                    DayOfWeek.Wednesday,
                    theather.OpeningTime,
                    night,
                    38
                );

                table.AddPricingRule(
                    DayOfWeek.Wednesday,
                    night,
                    theather.ClosingTime,
                    39
                );

                table.AddPricingRule(
                    DayOfWeek.Thursday,
                    theather.OpeningTime,
                    night,
                    45
                );

                table.AddPricingRule(
                    DayOfWeek.Thursday,
                    night,
                    theather.ClosingTime,
                    46
                );

                table.AddPricingRule(DayOfWeek.Friday, 46);

                table.AddPricingRule(
                    DayOfWeek.Saturday,
                    theather.OpeningTime,
                    night,
                    45
                );

                table.AddPricingRule(
                    DayOfWeek.Saturday,
                    night,
                    theather.ClosingTime,
                    46
                );

                table.AddPricingRule(
                    DayOfWeek.Sunday,
                    theather.OpeningTime,
                    night,
                    45
                );

                table.AddPricingRule(
                    DayOfWeek.Sunday,
                    night,
                    theather.ClosingTime,
                    46
                );
            }, Option3D.With3D);

            theather.HasDatePricingScheme(table =>
            {
                table.AddPricingRule(new DateTime(2019, 11, 15), 40);
                table.AddPricingRule(new DateTime(2019, 11, 20), 40);
                table.AddPricingRule(new DateTime(2019, 12, 25), 40);
                table.AddPricingRule(new DateTime(2020, 1, 1), 40);
            });

            theather.HasDatePricingScheme(table =>
            {
                var night = TimeSpan.FromHours(nightTime);

                table.AddPricingRule(
                    new DateTime(2019, 11, 15),
                    theather.OpeningTime,
                    night,
                    45
                );

                table.AddPricingRule(
                    new DateTime(2019, 11, 15),
                    night,
                    theather.ClosingTime,
                    46
                );

                table.AddPricingRule(
                    new DateTime(2019, 11, 20),
                    theather.OpeningTime,
                    night,
                    45
                );

                table.AddPricingRule(
                    new DateTime(2019, 11, 20),
                    night,
                    theather.ClosingTime,
                    46
                );

                table.AddPricingRule(
                    new DateTime(2019, 12, 25),
                    theather.OpeningTime,
                    night,
                    45
                );

                table.AddPricingRule(
                    new DateTime(2019, 12, 25),
                    night,
                    theather.ClosingTime,
                    46
                );

                table.AddPricingRule(
                    new DateTime(2020, 1, 1),
                    theather.OpeningTime,
                    night,
                    45
                );

                table.AddPricingRule(
                    new DateTime(2020, 1, 1),
                    night,
                    theather.ClosingTime,
                    46
                );

            }, Option3D.With3D);
        }
    }
}