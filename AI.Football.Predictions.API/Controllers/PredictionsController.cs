using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Data;
using AI.Football.Predictions.API.Models;
using AI.Football.Predictions.API.Services;
using AI.Football.Predictions.API.Services.Interfaces;
using AI.Football.Predictions.ML.Models;
using AI.Football.Predictions.ML.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AI.Football.Predictions.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionsController : ControllerBase
    {
        private readonly MatchDataProcessor _matchDataProcessor;
        private readonly IMatchPredictionService _matchPredictionService;
        private readonly IPredictionDataService _predictionDataService;
        private readonly DataContext _context;

        public PredictionsController(MatchDataProcessor matchDataProcessor, IMatchPredictionService matchPredictionService, IPredictionDataService predictionDataService, DataContext context)
        {
            _matchDataProcessor = matchDataProcessor;
            _matchPredictionService = matchPredictionService;
            _predictionDataService = predictionDataService;
            _context = context;
        }

        [HttpPost("Process-data")]
        public async Task<IActionResult> ImportTrainingData()
        {
            await _matchDataProcessor.ProcessAndSaveMatchDataAsync();
            return Ok("Dane zostały zaimportowane i zapisane.");
        }

        [HttpPost("Train")]
        public async Task<IActionResult> TrainModel()
        {
            var matchData = await _context.HistoricalMatches
                .AsNoTracking()
                .Select(m => new MatchData
                {
                    HomeGoalsAvg = m.HomeStatistics.AvgGoals,
                    AwayGoalsAvg = m.AwayStatistics.AvgGoals,
                    HomePossessionAvg = m.HomeStatistics.AvgPossession,
                    AwayPossessionAvg = m.AwayStatistics.AvgPossession,
                    HomeShotsAvg = m.HomeStatistics.AvgShots,
                    AwayShotsAvg = m.AwayStatistics.AvgShots,
                    HomeAvgXG = m.HomeStatistics.AvgXG,
                    AwayAvgXG = m.AwayStatistics.AvgXG,
                    HomeAvgXGA = m.HomeStatistics.AvgXGA,
                    AwayAvgXGA = m .AwayStatistics.AvgXGA,
                    HomeWinRate = m.HomeStatistics.WinRate,
                    AwayWinRate = m.AwayStatistics.WinRate,
                    H2HHomeWins = m.H2HHomeWins,
                    H2HAwayWins = m.H2HAwayWins,
                    H2HDraws = m.H2HDraws,
                    H2HHomeWinRate = m.H2HHomeWinRate,
                    H2HAwayWinRate = m.H2HAwayWinRate,
                    H2HDrawRate = m.H2HDrawRate,
                    H2HHomeAvgXG = m.H2HHomeStatistics.AvgXG,
                    H2HHomeAvgXGA = m.H2HHomeStatistics.AvgXGA,
                    H2HAwayAvgXG = m.H2HAwayStatistics.AvgXG,
                    H2HAwayAvgXGA = m.H2HAwayStatistics.AvgXGA,
                    H2HHomeAvgBigChances = m.H2HHomeStatistics.AvgBigChances,
                    H2HAwayAvgBigChances = m.H2HAwayStatistics.AvgBigChances,
                    H2HHomeAvgCorners = m.H2HHomeStatistics.AvgCorners,
                    H2HAwayAvgCorners = m.H2HAwayStatistics.AvgCorners,
                    H2HHomeAvgFreeKicks = m.H2HHomeStatistics.AvgFreeKicks,
                    H2HAwayAvgFreeKicks = m.H2HAwayStatistics.AvgFreeKicks,
                    H2HHomeAvgRedCards = m.H2HHomeStatistics.AvgRedCards,
                    H2HAwayAvgRedCards = m.H2HAwayStatistics.AvgRedCards,
                    HomeScore = m.HomeCompetitor.Score,
                    AwayScore = m.AwayCompetitor.Score,
                    MatchResult = (uint)m.Result
                }).ToListAsync();

            if (matchData == null || !matchData.Any())
            {
                return BadRequest("Brak danych do trenowania modelu.");
            }

            _matchPredictionService.Train(matchData);
            return Ok("Model został wytrenowany pomyślnie.");
        }

        [HttpGet("Accuracy")]
        public async Task<ActionResult<PredictionAccuracy>> GetPredictionStatistics()
        {
            var predictionAccuracy = await _predictionDataService.GetPredictionAccuracy();
            return Ok(predictionAccuracy);
        }

    }
}