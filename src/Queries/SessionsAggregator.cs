using System.Collections.Generic;
using Exemplo.Models.Cinema;

namespace Exemplo.Queries
{
    public class SessionsAggregator
    {
        public string TheatherName { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
    }
}