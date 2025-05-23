using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.ML.Models;

namespace AI.Football.Predictions.ML.Services
{
    public interface IMatchPredictionService
    {
        void TrainClassification(IEnumerable<MatchData> matchData);
        MatchPrediction Predict(MatchData matchData);

        void TrainRegression(IEnumerable<MatchData> matchData);
        MatchScorePrediction PredictScore(MatchData matchData);
    }
}