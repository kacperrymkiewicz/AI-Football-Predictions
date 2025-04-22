using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AI.Football.Predictions.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionsController : ControllerBase
    {
        private readonly MatchDataProcessor _matchDataProcessor;
        public PredictionsController(MatchDataProcessor matchDataProcessor)
        {
            _matchDataProcessor = matchDataProcessor;
        }

        [HttpPost("Process-data")]
        public async Task<IActionResult> ImportTrainingData()
        {
            await _matchDataProcessor.ProcessAndSaveMatchDataAsync();
            return Ok("Dane zostały zaimportowane i zapisane.");
        }
    }
}