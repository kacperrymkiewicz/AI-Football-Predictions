using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Football.Predictions.ML.Data
{
    public class MatchPrediction
    {
        public float HomeWinProbability { get; set; }
        public float DrawProbability { get; set; }
        public float AwayWinProbability { get; set; }
    }
}
