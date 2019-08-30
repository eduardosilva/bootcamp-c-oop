using System.Collections.Generic;
using Exemplo.Enums;

namespace Exemplo.Models.Cinema
{
    public class Room
    {
        public int Number { get; set; }
        public ScreenType ScreenType { get; set; }
        public IEnumerable<Row> Rows { get; set; }
        public bool Supports3D { get; set; }
    }
}