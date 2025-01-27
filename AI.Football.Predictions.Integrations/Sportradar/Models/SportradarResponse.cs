using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.Sportradar.Models
{
    public class SportradarResponse
    {
        public long LastUpdateId { get; set; }
        public int RequestedUpdateId { get; set; }
        public int Ttl { get; set; }
        public List<Sport> Sports { get; set; }
        public List<Country> Countries { get; set; }
        public List<Competition> Competitions { get; set; }
        public List<Competitor> Competitors { get; set; }
        public List<Game> Games { get; set; }
        public List<Bookmaker> Bookmakers { get; set; }
    }

}