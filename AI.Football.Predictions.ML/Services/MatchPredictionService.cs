using AI.Football.Predictions.ML.Models;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.ML.Services
{
    public class MatchPredictionService : IMatchPredictionService
    {
        private readonly MLContext _mlContext;

        private ITransformer? _classificationModel;
        private ITransformer? _homeRegressionModel;
        private ITransformer? _awayRegressionModel;

        private readonly string _classificationModelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "MatchPredictionModel.zip");
        private readonly string _homeRegressionModelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "HomeScoreRegressionModel.zip");
        private readonly string _awayRegressionModelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "AwayScoreRegressionModel.zip");

        public MatchPredictionService()
        {
            _mlContext = new MLContext(seed: 0);

            if (File.Exists(_classificationModelPath))
                _classificationModel = _mlContext.Model.Load(_classificationModelPath, out _);

            if (File.Exists(_homeRegressionModelPath))
                _homeRegressionModel = _mlContext.Model.Load(_homeRegressionModelPath, out _);

            if (File.Exists(_awayRegressionModelPath))
                _awayRegressionModel = _mlContext.Model.Load(_awayRegressionModelPath, out _);
        }

        public void TrainClassification(IEnumerable<MatchData> matchData)
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
                    nameof(MatchData.H2HHomeAvgCorners),
                    nameof(MatchData.H2HAwayAvgCorners),
                    nameof(MatchData.H2HHomeAvgFreeKicks),
                    nameof(MatchData.H2HAwayAvgFreeKicks),
                    nameof(MatchData.H2HHomeAvgRedCards),
                    nameof(MatchData.H2HAwayAvgRedCards)))
                .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(_mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy("Label", "Features", exampleWeightColumnName: "Weight"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var trainedModel = pipeline.Fit(data);
            _mlContext.Model.Save(trainedModel, data.Schema, _classificationModelPath);
            _classificationModel = trainedModel;
        }

        public void TrainRegression(IEnumerable<MatchData> matchData)
        {
            var cleanedData = matchData.Where(x => x.HomeScore >= 0 && x.AwayScore >= 0).ToList();
            var data = _mlContext.Data.LoadFromEnumerable(cleanedData);

            var pipeline = _mlContext.Transforms.Concatenate("Features",
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
                    nameof(MatchData.H2HHomeAvgCorners),
                    nameof(MatchData.H2HAwayAvgCorners),
                    nameof(MatchData.H2HHomeAvgFreeKicks),
                    nameof(MatchData.H2HAwayAvgFreeKicks),
                    nameof(MatchData.H2HHomeAvgRedCards),
                    nameof(MatchData.H2HAwayAvgRedCards))
                .Append(_mlContext.Transforms.NormalizeMinMax("Features"));

            var homePipeline = pipeline.Append(_mlContext.Regression.Trainers.LbfgsPoissonRegression(labelColumnName: nameof(MatchData.HomeScore), featureColumnName: "Features"));
            var awayPipeline = pipeline.Append(_mlContext.Regression.Trainers.LbfgsPoissonRegression(labelColumnName: nameof(MatchData.AwayScore), featureColumnName: "Features"));

            _homeRegressionModel = homePipeline.Fit(data);
            _awayRegressionModel = awayPipeline.Fit(data);

            _mlContext.Model.Save(_homeRegressionModel, data.Schema, _homeRegressionModelPath);
            _mlContext.Model.Save(_awayRegressionModel, data.Schema, _awayRegressionModelPath);
        }

        public MatchPrediction Predict(MatchData matchData)
        {
            if (_classificationModel == null)
                throw new InvalidOperationException("Classification model not loaded!");

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<MatchData, MatchPrediction>(_classificationModel);
            return predictionEngine.Predict(matchData);
        }

        public MatchScorePrediction PredictScore(MatchData matchData)
        {
            if (_homeRegressionModel == null || _awayRegressionModel == null)
                throw new InvalidOperationException("Score regression models not loaded!");

            var homePredictionEngine = _mlContext.Model.CreatePredictionEngine<MatchData, ScoreOutput>(_homeRegressionModel);
            var awayPredictionEngine = _mlContext.Model.CreatePredictionEngine<MatchData, ScoreOutput>(_awayRegressionModel);

            var home = homePredictionEngine.Predict(matchData).Score;
            var away = awayPredictionEngine.Predict(matchData).Score;

            return new MatchScorePrediction
            {
                HomeScore = (int)Math.Round(home),
                AwayScore = (int)Math.Round(away)
            };
        }
    }
}
