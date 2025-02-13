using System.Collections.Generic; 
using System; 
namespace AI.Football.Predictions.Integrations.Sportradar.Models{ 

    public class RecentGame
    {
        public int Id { get; set; }
        public int SportId { get; set; }
        public int CompetitionId { get; set; }
        public int SeasonNum { get; set; }
        public int StageNum { get; set; }
        public int GroupNum { get; set; }
        public string CompetitionDisplayName { get; set; }
        public DateTime StartTime { get; set; }
        public int StatusGroup { get; set; }
        public string StatusText { get; set; }
        public string ShortStatusText { get; set; }
        public int GameTimeAndStatusDisplayType { get; set; }
        public HomeCompetitor HomeCompetitor { get; set; }
        public AwayCompetitor AwayCompetitor { get; set; }
        public int Outcome { get; set; }
        public List<ExtraDatum> ExtraData { get; set; }
        public int Winner { get; set; }
        public List<double> Scores { get; set; }
        public int HomeAwayTeamOrder { get; set; }
        public bool HasPointByPoint { get; set; }
        public int? RoundNum { get; set; }
    }

}