using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.API.Models
{
    public class MatchTrainingData
    {
        public int Id { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public float HomePossession { get; set; }
        public float AwayPossession { get; set; }
        public uint MatchResult { get; set; }

        public override string ToString()
        {
            return $"HomeGoalsAvg: {HomeGoals}, AwayGoalsAvg: {AwayGoals}, Result: {MatchResult}";
        }
    }
}