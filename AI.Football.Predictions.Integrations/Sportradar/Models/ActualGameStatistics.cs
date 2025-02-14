using System.Collections.Generic; 
namespace AI.Football.Predictions.Integrations.Sportradar.Models
{ 
    public class ActualGameStatistics
    {
        public int FilterId { get; set; }
        public ActualPlayTime ActualPlayTime { get; set; }
        public List<General> General { get; set; }
        public AddedTime AddedTime { get; set; }
        public WastedTime WastedTime { get; set; }
    }

}