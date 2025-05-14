using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AI.Football.Predictions.API.Models
{
    [Owned]
    public class H2HRecentStatistics
    {
        public float AvgGoals { get; set; }
        public float AvgShots { get; set; }
        public float AvgPossession { get; set; }
        public float AvgFouls { get; set; }
        public float AvgXG { get; set; }
        public float AvgXGA { get; set; }
        public float AvgBigChances { get; set; }
        public float AvgShotsOnTarget { get; set; }
        public float AvgCorners { get; set;}
        public float AvgFreeKicks { get; set; }
        public float AvgRedCards { get; set; }
    }
}