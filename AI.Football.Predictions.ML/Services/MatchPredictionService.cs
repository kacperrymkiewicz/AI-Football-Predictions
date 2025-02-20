using AI.Football.Predictions.ML.Models;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AI.Football.Predictions.ML.Services
{
    public class MatchPredictionService
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;
        private readonly string _modelPath = "Models/MatchPredictionModel.zip";

        public MatchPredictionService()
        {
            _mlContext = new MLContext(seed: 0);
        }

        public void Train(string dataPath)
        {
            var data = _mlContext.Data.LoadFromTextFile<MatchData>(dataPath, separatorChar: ',', hasHeader: true);
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label")
                .Append(_mlContext.Transforms.Concatenate("Features", 
                    nameof(MatchData.HomeGoalsAvg),
                    nameof(MatchData.AwayGoalsAvg), 
                    nameof(MatchData.HomePossessionAvg),
                    nameof(MatchData.AwayPossessionAvg), 
                    nameof(MatchData.HomeShotsAvg),
                    nameof(MatchData.AwayShotsAvg), 
                    nameof(MatchData.HomeWinRate),
                    nameof(MatchData.AwayWinRate), 
                    nameof(MatchData.H2HHomeWins),
                    nameof(MatchData.H2HAwayWins), 
                    nameof(MatchData.H2HDraws)))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var trainedModel = pipeline.Fit(data);
            _mlContext.Model.Save(trainedModel, data.Schema, _modelPath);
            _model = trainedModel;
        }

        public MatchPrediction Predict(MatchData matchData)
        {
            if (_model == null && File.Exists(_modelPath))
            {
                _model = _mlContext.Model.Load(_modelPath, out _);
            }

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<MatchData, MatchPrediction>(_model);
            return predictionEngine.Predict(matchData);
        }
    }
}