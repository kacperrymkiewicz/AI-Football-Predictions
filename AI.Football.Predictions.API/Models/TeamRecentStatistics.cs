using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AI.Football.Predictions.API.Models
{
    [Owned]
    public class TeamRecentStatistics
    {
        public int LastMatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public float WinRate => LastMatchesPlayed == 0 ? 0 : (float)Wins / LastMatchesPlayed;
        public float AvgGoals { get; set; }
        public float AvgShots { get; set; }
        public float AvgPossession { get; set; }
        public float AvgFouls { get; set; }
        public float AvgXG { get; set; }
        public float AvgXGA { get; set; }
    }
}