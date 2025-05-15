using AI.Football.Predictions.ML.Models;
using Microsoft.ML;
using Microsoft.ML.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AI.Football.Predictions.ML.Services
{
    public class MatchPredictionService : IMatchPredictionService
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;
        private readonly string _modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "MatchPredictionModel.zip");

        public MatchPredictionService()
        {
            _mlContext = new MLContext(seed: 0);
            if (File.Exists(_modelPath))
            {
                _model = _mlContext.Model.Load(_modelPath, out _);
            }
            else
            {
                throw new Exception($"Model was not found. Check if the file exists: {_modelPath}");
            }
        }

        public void Train(IEnumerable<MatchData> matchData)
        {
            var data = _mlContext.Data.LoadFromEnumerable(matchData);
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label", keyOrdinality: ValueToKeyMappingEstimator.KeyOrdinality.ByValue)
                .Append(_mlContext.Transforms.Concatenate("Features", 
                    nameof(MatchData.HomeGoalsAvg),
                    nameof(MatchData.AwayGoalsAvg), 
                    nameof(MatchData.HomePossessionAvg),
                    nameof(MatchData.AwayPossessionAvg), 
                    nameof(MatchData.HomeShotsAvg),
                    nameof(MatchData.AwayShotsAvg), 
                    nameof(MatchData.HomeAvgXG), 
                    nameof(MatchData.AwayAvgXG), 
                    nameof(MatchData.HomeAvgXGA), 
                    nameof(MatchData.AwayAvgXGA), 
                    nameof(MatchData.HomeWinRate),
                    nameof(MatchData.AwayWinRate), 
                    nameof(MatchData.H2HHomeWins),
                    nameof(MatchData.H2HAwayWins), 
                    nameof(MatchData.H2HDraws),
                    nameof(MatchData.H2HHomeWinRate), 
                    nameof(MatchData.H2HAwayWinRate), 
                    nameof(MatchData.H2HDrawRate),
                    nameof(MatchData.H2HHomeAvgXG),
                    nameof(MatchData.H2HHomeAvgXGA),
                    nameof(MatchData.H2HAwayAvgXG),
                    nameof(MatchData.H2HAwayAvgXGA),
                    nameof(MatchData.H2HHomeAvgBigChances),
                    nameof(MatchData.H2HAwayAvgBigChances),
                    // nameof(MatchData.H2HHomeAvgCorners),
                    // nameof(MatchData.H2HAwayAvgCorners),
                    nameof(MatchData.H2HHomeAvgFreeKicks),
                    nameof(MatchData.H2HAwayAvgFreeKicks),
                    nameof(MatchData.H2HHomeAvgRedCards),
                    nameof(MatchData.H2HAwayAvgRedCards)))
                .Append(_mlContext.Transforms.NormalizeMinMax("Features", "Features"))
                .Append(_mlContext.Transforms.NormalizeLogMeanVariance("Features", "Features"))
                .Append(_mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy("Label", "Features", exampleWeightColumnName: "Weight"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var trainedModel = pipeline.Fit(data);
            _mlContext.Model.Save(trainedModel, data.Schema, _modelPath);
            _model = trainedModel;
        }

        public MatchPrediction Predict(MatchData matchData)
        {
            if (_model == null)
                throw new InvalidOperationException("Model was not loaded!");

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<MatchData, MatchPrediction>(_model);
            return predictionEngine.Predict(matchData);
        }
    }
}