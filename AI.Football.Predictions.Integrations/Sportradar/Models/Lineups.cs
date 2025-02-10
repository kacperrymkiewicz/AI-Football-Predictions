using System.Collections.Generic; 
namespace AI.Football.Predictions.Integrations.Sportradar.Models
{ 
    public class Lineups
    {
        public string Status { get; set; }
        public string Formation { get; set; }
        public bool HasFieldPositions { get; set; }
        public List<Member> Members { get; set; }
        public List<StatsCategory> StatsCategory { get; set; }
    }

}