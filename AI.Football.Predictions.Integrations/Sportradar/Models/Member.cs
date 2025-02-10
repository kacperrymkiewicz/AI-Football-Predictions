using System.Collections.Generic; 
namespace AI.Football.Predictions.Integrations.Sportradar.Models
{ 
    public class Member
    {
        public int Status { get; set; }
        public string StatusText { get; set; }
        public Position Position { get; set; }
        public Formation Formation { get; set; }
        public YardFormation YardFormation { get; set; }
        public double Ranking { get; set; }
        public bool HasStats { get; set; }
        public List<Stat> Stats { get; set; }
        public int PopularityRank { get; set; }
        public int CompetitorId { get; set; }
        public int NationalId { get; set; }
        public int Id { get; set; }
        public bool? HasHighestRanking { get; set; }
        public Substitution Substitution { get; set; }
        public Injury Injury { get; set; }
        public int AthleteId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int JerseyNumber { get; set; }
        public string NameForURL { get; set; }
        public int ImageVersion { get; set; }
    }

}