namespace AI.Football.Predictions.Integrations.Sportradar.Models
{
    public class Statistic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompetitorId { get; set; }
        public bool IsMajor { get; set; }
        public string Value { get; set; }
        public double ValuePercentage { get; set; }
        public bool IsPrimary { get; set; }
        public int Order { get; set; }
        public int MarkedTeam { get; set; }
        public bool IsTop { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryOrder { get; set; }
    }

}