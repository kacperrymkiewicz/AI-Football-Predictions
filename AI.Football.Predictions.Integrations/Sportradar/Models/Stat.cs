using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.Sportradar.Models
{
    public class Stat
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int Id { get; set; }
        public int CompetitorId { get; set; }
        public bool IsMajor { get; set; }
        public double ValuePercentage { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsTop { get; set; }
    }

}