using System.Collections.Generic;
using Exemplo.Enums;

namespace Exemplo.Models
{
    public class Room
    {
        public ScreenType ScreenType { get; set; }
        public IEnumerable<Row> Rows { get; set; }
        public bool Supports3D { get; set; }
    }
}