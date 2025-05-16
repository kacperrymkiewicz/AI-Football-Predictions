using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Data;
using AI.Football.Predictions.API.Mappings;
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
                .Include(m => m.HomeCompetitor)
                .Include(m => m.AwayCompetitor)
                .AsNoTracking()
                .Select(m => MatchDataMapper.FromHistoricalMatch(m))
                .ToListAsync();

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