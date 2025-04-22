using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AI.Football.Predictions.API.Models
{
    [Owned]
    public class TeamStatistics
    {
        public float AvgGoals { get; set; }
        public float ShotsPerGame { get; set; }
        public float BallPossession { get; set; }
        public float Fouls { get; set; }

    }
}