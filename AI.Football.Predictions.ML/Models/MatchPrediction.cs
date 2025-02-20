using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Football.Predictions.ML.Models
{
    public class MatchPrediction
    {
        [ColumnName("PredictedLabel")] public uint PredictedResult { get; set; }
        public float[] Score { get; set; }
    }
}
