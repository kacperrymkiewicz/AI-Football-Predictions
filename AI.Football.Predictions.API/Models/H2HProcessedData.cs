using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.API.Models
{
    public class H2HProcessedData
    {
        public int H2HHomeWins { get; set;}
        public int H2HAwayWins { get; set; } 
        public int H2HDraws { get; set; }
        public H2HRecentStatistics H2HHomeStatistics { get; set; }
        public H2HRecentStatistics H2HAwayStatistics { get; set; }
    }
}