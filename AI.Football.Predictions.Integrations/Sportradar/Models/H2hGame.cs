using System.Collections.Generic; 
using System; 
namespace AI.Football.Predictions.Integrations.Sportradar.Models
{ 
    public class H2hGame
    {
        public int Id { get; set; }
        public int SportId { get; set; }
        public int CompetitionId { get; set; }
        public int SeasonNum { get; set; }
        public int StageNum { get; set; }
        public int GroupNum { get; set; }
        public string StageName { get; set; }
        public string CompetitionDisplayName { get; set; }
        public DateTime StartTime { get; set; }
        public int StatusGroup { get; set; }
        public string StatusText { get; set; }
        public string ShortStatusText { get; set; }
        public int GameTimeAndStatusDisplayType { get; set; }
        public bool JustEnded { get; set; }
        public double GameTime { get; set; }
        public string GameTimeDisplay { get; set; }
        public bool HasTVNetworks { get; set; }
        public string AggregateText { get; set; }
        public string ShortAggregateText { get; set; }
        public HomeCompetitor HomeCompetitor { get; set; }
        public AwayCompetitor AwayCompetitor { get; set; }
        public bool IsHomeAwayInverted { get; set; }
        public bool HasStandings { get; set; }
        public string StandingsName { get; set; }
        public bool HasBrackets { get; set; }
        public bool HasPreviousMeetings { get; set; }
        public bool HasRecentMatches { get; set; }
        public int Winner { get; set; }
        public int HomeAwayTeamOrder { get; set; }
        public bool HasPointByPoint { get; set; }
        public List<double> Scores { get; set; }
        public int? RoundNum { get; set; }
    }

}