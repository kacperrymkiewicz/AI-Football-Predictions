using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.FootballData.Models
{
    public class Match
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime UtcDate { get; set; }
        public HomeTeam HomeTeam { get; set; }
        public AwayTeam AwayTeam { get; set; }
        public Score Score { get; set; }
        public Area Area { get; set; }
        public Competition Competition { get; set; }
        public Season Season { get; set; }
        public int Matchday { get; set; }
        public string Stage { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}