using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Data;
using AI.Football.Predictions.API.Mappings;
using AI.Football.Predictions.API.Models;
using AI.Football.Predictions.API.Services.Interfaces;
using AI.Football.Predictions.ML.Models;
using AI.Football.Predictions.ML.Services;
using Microsoft.EntityFrameworkCore;

namespace AI.Football.Predictions.API.Services
{
    public class PredictionDataService : IPredictionDataService
    {
        private readonly IMatchPredictionService _matchPredictionService;
        private readonly IMatchService _matchService;
        private readonly DataContext _context;

        public PredictionDataService(IMatchPredictionService matchPredictionService, IMatchService matchService, DataContext context)
        {
            _matchPredictionService = matchPredictionService;
            _matchService = matchService;
            _context = context;
        }

        public async Task<PredictionAccuracy> GetPredictionAccuracy()
        {
            var matchData = await _context.HistoricalMatches
                .Include(m => m.HomeCompetitor)
                .Include(m => m.AwayCompetitor)
                .AsNoTracking()
                .ToListAsync();
            var predictionAccuracy = new PredictionAccuracy();

            foreach (var match in matchData)
            {
                MatchData predictionData = MatchDataMapper.FromHistoricalMatch(match);
            
                var prediction = _matchPredictionService.Predict(predictionData);
                var scorePrediction = _matchPredictionService.PredictScoreWithConsistency(predictionData);
                predictionAccuracy.TotalPredictions++;

                if((Result)prediction.PredictedResult == match.Result)
                {
                    predictionAccuracy.CorrectPredictions++;
                }

                if (scorePrediction.HomeScore == match.HomeCompetitor.Score && scorePrediction.AwayScore == match.AwayCompetitor.Score)
                {
                    predictionAccuracy.ScoreCorrectPredictions++;    
                }

                if (Math.Abs(scorePrediction.HomeScore - match.HomeCompetitor.Score) <= 1 && Math.Abs(scorePrediction.AwayScore - match.AwayCompetitor.Score) <= 1)
                {
                    predictionAccuracy.ScoreClosePredictions++;
                }

                switch ((Result)prediction.PredictedResult)
                {
                    case Result.Home:
                        predictionAccuracy.HomeWinPredictions++;
                        if (match.Result == Result.Home) predictionAccuracy.HomeWinCorrect++;
                        break;

                    case Result.Draw:
                        predictionAccuracy.DrawPredictions++;
                        if (match.Result == Result.Draw) predictionAccuracy.DrawCorrect++;
                        break;

                    case Result.Away:
                        predictionAccuracy.AwayWinPredictions++;
                        if (match.Result == Result.Away) predictionAccuracy.AwayWinCorrect++;
                        break;
                }
            }

            return predictionAccuracy;
        }
    }
}