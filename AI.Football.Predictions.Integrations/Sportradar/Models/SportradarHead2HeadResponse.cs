using System.Collections.Generic; 
namespace AI.Football.Predictions.Integrations.Sportradar.Models{ 

    public class SportradarHead2HeadResponse
    {
        public Game Game { get; set; }
        public List<Sport> Sports { get; set; }
        public List<Country> Countries { get; set; }
        public List<Competition> Competitions { get; set; }
    }

}