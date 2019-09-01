using System;

namespace Exemplo.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime RoundToNext(this DateTime date, int minutes)
        {
            var minuteTicks = TimeSpan.TicksPerMinute * minutes;
            return new DateTime((date.Ticks + minuteTicks - 1) / minuteTicks * minuteTicks, date.Kind);
        }
    }
}