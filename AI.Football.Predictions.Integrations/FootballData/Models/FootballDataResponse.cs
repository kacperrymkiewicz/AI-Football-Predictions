using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.FootballData.Models
{
    public class FootballDataResponse
    {
        public Filters Filters { get; set; }
        public ResultSet ResultSet { get; set; }
        public List<Match> Matches { get; set; }
    }
}