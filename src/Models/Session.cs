using System;

namespace Exemplo.Models
{
    public class Session
    {
        public Room Room { get; set; }
        public Film Film { get; set; }
        public DateTime StartDate { get; set; }
        public bool Session3D { get; set; }
    }
}