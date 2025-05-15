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
        public float HomeGoalsAvg { get; set; }
        public float AwayGoalsAvg { get; set; }
        public float HomePossessionAvg { get; set; }
        public float AwayPossessionAvg { get; set; }
        public float HomeShotsAvg { get; set; }
        public float AwayShotsAvg { get; set; }
        public float HomeAvgXG { get; set; }
        public float AwayAvgXG { get; set; }
        public float HomeAvgXGA { get; set; }
        public float AwayAvgXGA { get; set; }
        public float HomeWinRate { get; set; }
        public float AwayWinRate { get; set; }
        public float H2HHomeWins { get; set; }
        public float H2HAwayWins { get; set; }
        public float H2HDraws { get; set; }
        public float H2HHomeWinRate { get; set; }
        public float H2HAwayWinRate { get; set; }
        public float H2HDrawRate { get; set; }
        public float H2HHomeAvgXG { get; set; }
        public float H2HHomeAvgXGA { get; set; }
        public float H2HAwayAvgXG { get; set; }
        public float H2HAwayAvgXGA { get; set; }
        public float H2HHomeAvgBigChances { get; set; }
        public float H2HAwayAvgBigChances { get; set; }
        public float H2HHomeAvgCorners { get; set; }
        public float H2HAwayAvgCorners { get; set; }
        public float H2HHomeAvgFreeKicks { get; set; }
        public float H2HAwayAvgFreeKicks { get; set; }
        public float H2HHomeAvgRedCards { get; set; }
        public float H2HAwayAvgRedCards { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public float Weight => MatchResult == 2 ? 0.7f : 1f;

        [ColumnName("Label")]
        public uint MatchResult { get; set; }
    }
}
