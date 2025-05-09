using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Data;
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
            var matchData = await _context.HistoricalMatches.ToListAsync();
            var predictionAccuracy = new PredictionAccuracy();

            foreach (var match in matchData)
            {
                MatchData predictionData = new MatchData
                {
                    HomeGoalsAvg = match.HomeStatistics.AvgGoals,
                    AwayGoalsAvg = match.AwayStatistics.AvgGoals,
                    HomePossessionAvg = match.HomeStatistics.AvgPossession,
                    AwayPossessionAvg = match.AwayStatistics.AvgPossession,
                    HomeShotsAvg = match.HomeStatistics.AvgShots,
                    AwayShotsAvg = match.AwayStatistics.AvgShots,
                    HomeWinRate = match.HomeStatistics.WinRate,
                    AwayWinRate = match.AwayStatistics.WinRate,
                    H2HHomeWins = match.H2HHomeWins,
                    H2HAwayWins = match.H2HAwayWins,
                    H2HDraws = match.H2HDraws,
                    H2HHomeWinRate = match.H2HHomeWinRate,
                    H2HAwayWinRate = match.H2HAwayWinRate,
                    H2HDrawRate = match.H2HDrawRate
                };
            
                var prediction = _matchPredictionService.Predict(predictionData);
                predictionAccuracy.TotalPredictions++;

                if((Result)prediction.PredictedResult == match.Result)
                {
                    predictionAccuracy.CorrectPredictions++;
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