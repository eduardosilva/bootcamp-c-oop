using System;
using System.Collections.Generic;
using System.Linq;
using Exemplo.Enums;


namespace Exemplo.Models.Cinema.Helpers
{
    public class RoomLayoutBuilder
    {
        private ICollection<RowLayoutBuilder> rowBuilders = new List<RowLayoutBuilder>();

        public RoomLayoutBuilder HasRow(char code, Action<RowLayoutBuilder> builder)
        {
            var rowBuilder = new RowLayoutBuilder(code);
            
            builder(rowBuilder);

            rowBuilders.Add(rowBuilder);

            return this;
        }

        public IEnumerable<Row> Build()
        {
            return this.rowBuilders.Select(b => b.Build()).ToList();
        }        
    }

    public class RowLayoutBuilder
    {
        private char code;
        private IEnumerable<int> seatNumbers;
        private IDictionary<int, Action<Seat>> seatBuilders = new Dictionary<int, Action<Seat>>();

        public RowLayoutBuilder(char code) {
            this.code = code;
        }

        public RowLayoutBuilder HasSeats(IEnumerable<int> numbers)
        {
            if (numbers.Distinct().Count() != numbers.Count())
            {
                throw new ApplicationException("Duplicate seat numbers in definition.");
            }

            this.seatNumbers = numbers;

            return this;
        }

        public RowLayoutBuilder HasSeats(params int[] args)
        {
            if (args.Distinct().Count() != args.Count())
            {
                throw new ApplicationException("Duplicate seat numbers in definition.");
            }

            this.seatNumbers = args;

            return this;
        }

        public RowLayoutBuilder HasSeatRange(int start, int count)
        {
            this.seatNumbers = Enumerable.Range(start, count);
            return this;
        }

        public RowLayoutBuilder WithSeatAt(int number, Action<Seat> seatBuilder)
        {
            seatBuilders.Remove(number);
            seatBuilders.Add(number, seatBuilder);
            return this;
        }

        public Row Build()
        {
            var row = new Row() { Letter = this.code };

            var seats = this.seatNumbers.Select(n => {
                var seat = new Seat(row) {
                    Number = n,
                    Type = SeatType.Regular
                };
                Action<Seat> builder = null;

                if (this.seatBuilders.TryGetValue(n, out builder))
                {
                    builder(seat);
                }

                seat.Number = n;

                return seat;
            }).ToList();

            row.Seats = seats;
            return row;
        }
    }
}
