namespace AI.Football.Predictions.Integrations.Sportradar.Models
{
    public class Substitution
    {
        public int PlayerId { get; set; }
        public double Time { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int EventOrder { get; set; }
        public double AddedTime { get; set; }
    }

}