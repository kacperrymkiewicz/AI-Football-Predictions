namespace AI.Football.Predictions.Integrations.Sportradar.Models
{ 
    public class PreciseGameTime
    {
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public bool AutoProgress { get; set; }
        public int ClockDirection { get; set; }
    }

}