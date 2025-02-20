using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Football.Predictions.ML.Models
{
    public class MatchData
    {
        [LoadColumn(0)]
        public float HomeGoalsAvg { get; set; }

        [LoadColumn(1)]
        public float AwayGoalsAvg { get; set; }

        [LoadColumn(2)]
        public float HomePossessionAvg { get; set; }

        [LoadColumn(3)]
        public float AwayPossessionAvg { get; set; }

        [LoadColumn(4)]
        public float HomeShotsAvg { get; set; }

        [LoadColumn(5)]
        public float AwayShotsAvg { get; set; }

        [LoadColumn(6)]
        public float HomeWinRate { get; set; }

        [LoadColumn(7)]
        public float AwayWinRate { get; set; }

        [LoadColumn(8)]
        public float H2HHomeWins { get; set; }

        [LoadColumn(9)]
        public float H2HAwayWins { get; set; }

        [LoadColumn(10)]
        public float H2HDraws { get; set; }

        [LoadColumn(11), ColumnName("Label")]
        public uint MatchResult { get; set; }
    }
}
