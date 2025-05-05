using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.Integrations.Sportradar.Models;

namespace AI.Football.Predictions.API.Models
{
    public class HistoricalMatch
    {
        public int Id { get; set; }
        public DateTime MatchDate { get; set; }
        public int MatchId { get; set; }

        public int HomeCompetitorId { get; set; }
        public Team HomeCompetitor { get; set; }
        public TeamRecentStatistics HomeStatistics { get; set; }

        public int AwayCompetitorId { get; set;}
        public Team AwayCompetitor { get; set; }
        public TeamRecentStatistics AwayStatistics { get; set; }


        public int H2HHomeWins { get; set; }
        public int H2HAwayWins { get; set; }
        public int H2HDraws { get; set; }

        public Result Result { get; set; }

        public override string ToString()
        {
            return $"{MatchDate}: HomeGoals: {HomeCompetitor.Score}, AwayGoalsAvg: {AwayCompetitor.Score}, Result: {Result}";
        }
    }
}