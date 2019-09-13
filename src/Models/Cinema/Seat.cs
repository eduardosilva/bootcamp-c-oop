using Exemplo.Enums;

namespace Exemplo.Models.Cinema
{
    public class Seat
    {
        protected Row RowParent { get; }
        public int Number { get; set; }
        public SeatType Type { get; set; }
        public string Name => $"{RowParent.Letter}{Number}";

        public Seat(Row row)
        {
            this.RowParent = row;
        }
    }
}