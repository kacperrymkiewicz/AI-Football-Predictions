using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Data;
using AI.Football.Predictions.API.Services;
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
        private readonly DataContext _context;

        public PredictionsController(MatchDataProcessor matchDataProcessor, IMatchPredictionService matchPredictionService, DataContext context)
        {
            _matchDataProcessor = matchDataProcessor;
            _matchPredictionService = matchPredictionService;
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
                    HomeWinRate = m.HomeStatistics.WinRate,
                    AwayWinRate = m.AwayStatistics.WinRate,
                    H2HHomeWins = m.H2HHomeWins,
                    H2HAwayWins = m.H2HAwayWins,
                    H2HDraws = m.H2HDraws,
                    MatchResult = (uint)m.Result
                }).ToListAsync();

            if (matchData == null || !matchData.Any())
            {
                return BadRequest("Brak danych do trenowania modelu.");
            }

            _matchPredictionService.Train(matchData);
            return Ok("Model został wytrenowany pomyślnie.");
        }

    }
}