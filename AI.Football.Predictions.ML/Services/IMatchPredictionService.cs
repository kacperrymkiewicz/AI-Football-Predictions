using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.ML.Models;

namespace AI.Football.Predictions.ML.Services
{
    public interface IMatchPredictionService
    {
        void Train(IEnumerable<MatchData> matchData);
        MatchPrediction Predict(MatchData matchData);
    }
}