using System.Collections.Generic; 
namespace AI.Football.Predictions.Integrations.Sportradar.Models
{ 
    public class Prediction
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public bool ShowVotes { get; set; }
        public int TotalVotes { get; set; }
        public Odds Odds { get; set; }
        public List<Option> Options { get; set; }
    }

}