using System;
using System.Collections.Generic;
using Exemplo.Enums;

namespace Exemplo.Models
{
    public class Film
    {
        public string Title { get; set; }
        public TimeSpan RunningTime { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool Available3D { get; set; }
    }
}