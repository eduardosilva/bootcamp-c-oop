using System.Collections.Generic;
using Exemplo.Enums;

namespace Exemplo.Models
{
    public class Row
    {        
        public char Letter { get; set; }
        public IEnumerable<Seat> Seats { get; set; }
    }
}
