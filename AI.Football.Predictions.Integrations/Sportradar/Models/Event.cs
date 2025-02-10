using System.Collections.Generic; 
namespace AI.Football.Predictions.Integrations.Sportradar.Models
{ 
    public class Event
    {
        public int CompetitorId { get; set; }
        public int StatusId { get; set; }
        public int StageId { get; set; }
        public int Order { get; set; }
        public int Num { get; set; }
        public double GameTime { get; set; }
        public int AddedTime { get; set; }
        public string GameTimeDisplay { get; set; }
        public int GameTimeAndStatusDisplayType { get; set; }
        public int PlayerId { get; set; }
        public bool IsMajor { get; set; }
        public EventType EventType { get; set; }
        public List<int> ExtraPlayers { get; set; }
    }

}