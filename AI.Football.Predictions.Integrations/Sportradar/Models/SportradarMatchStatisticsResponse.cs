using System.Collections.Generic; 
namespace AI.Football.Predictions.Integrations.Sportradar.Models
{
    public class SportradarMatchStatisticsResponse
    {
        public List<StatisticsFilter> StatisticsFilters { get; set; }
        public List<Statistic> Statistics { get; set; }
        public ActualGameStatistics ActualGameStatistics { get; set; }
        public long LastUpdateId { get; set; }
        public int RequestedUpdateId { get; set; }
        public int Ttl { get; set; }
        public List<Sport> Sports { get; set; }
        public List<Country> Countries { get; set; }
        public List<Competition> Competitions { get; set; }
        public List<Competitor> Competitors { get; set; }
        public List<Game> Games { get; set; }
    }

}