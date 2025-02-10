using System.Collections.Generic; 
namespace AI.Football.Predictions.Integrations.Sportradar.Models
{ 
    public class StatsCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderLevel { get; set; }
        public List<OrderByPosition> OrderByPosition { get; set; }
    }

}