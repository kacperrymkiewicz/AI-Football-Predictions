using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.Sportradar.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int SportId { get; set; }
        public int CompetitionId { get; set; }
        public int SeasonNum { get; set; }
        public int StageNum { get; set; }
        public int RoundNum { get; set; }
        public string RoundName { get; set; }
        public string CompetitionDisplayName { get; set; }
        public DateTime StartTime { get; set; }
        public int StatusGroup { get; set; }
        public string StatusText { get; set; }
        public string ShortStatusText { get; set; }
        public int GameTimeAndStatusDisplayType { get; set; }
        public bool JustEnded { get; set; }
        public double GameTime { get; set; }
        public string GameTimeDisplay { get; set; }
        public bool HasLineups { get; set; }
        public bool HasMissingPlayers { get; set; }
        public bool HasFieldPositions { get; set; }
        public bool HasTVNetworks { get; set; }
        public HomeCompetitor HomeCompetitor { get; set; }
        public AwayCompetitor AwayCompetitor { get; set; }
        public Venue Venue { get; set; }
        public bool IsHomeAwayInverted { get; set; }
        public bool HasStats { get; set; }
        public bool HasStandings { get; set; }
        public string StandingsName { get; set; }
        public bool HasBrackets { get; set; }
        public bool HasPreviousMeetings { get; set; }
        public bool HasRecentMatches { get; set; }
        public int Winner { get; set; }
        public int HomeAwayTeamOrder { get; set; }
    }

}