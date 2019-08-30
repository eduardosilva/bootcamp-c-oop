namespace Exemplo.Models.Booking
{
    public class PriceRange3D : IPriceRange
    {
        private int weekday;

        public PriceRangeWeekday3D(int weekday): base(weekday)
        {
            this.weekday = weekday;
        }

        public int Weekday { get => weekday; }

        public decimal GetPrice()
        {
            return 1;
        }
    }
}