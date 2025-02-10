namespace AI.Football.Predictions.Integrations.Sportradar.Models
{ 
    public class Stat
    {
        public int Type { get; set; }
        public string Value { get; set; }
        public bool IsTop { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int ImageId { get; set; }
        public string ShortName { get; set; }
    }

}