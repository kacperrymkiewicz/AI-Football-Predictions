using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.Sportradar.Models
{
    public class Odds
    {
        public int LineId { get; set; }
        public int GameId { get; set; }
        public int BookmakerId { get; set; }
        public int LineTypeId { get; set; }
        public LineType LineType { get; set; }
        public string Link { get; set; }
        public Bookmaker Bookmaker { get; set; }
        public List<Option> Options { get; set; }
    }

}