using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Services.Interfaces;
using AI.Football.Predictions.Integrations.FootballData.Models;
using AI.Football.Predictions.Integrations.FootballData.Services;
using AI.Football.Predictions.Integrations.Sportradar.Models;
using AI.Football.Predictions.Integrations.Sportradar.Services;
using AI.Football.Predictions.ML.Models;
using AI.Football.Predictions.ML.Services;
using Microsoft.AspNetCore.Mvc;

namespace AI.Football.Predictions.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly IFootballDataApiService _footballDataService;
        private readonly ISportradarApiService _sportradarService;
        private readonly IMatchPredictionService _predictionService;
        private readonly IMatchService _matchService;


        public MatchesController(IFootballDataApiService footballDataService, ISportradarApiService sportradarService, IMatchPredictionService predictionService, IMatchService matchService)
        {
            _footballDataService = footballDataService;
            _sportradarService = sportradarService;
            _predictionService = predictionService;
            _matchService = matchService;
        }

        [HttpGet(Name = "GetMatches")]
        [EndpointSummary("Gets the list of football matches")]
        public async Task<ActionResult<SportradarResponse>> GetMatches([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var matches = await _sportradarService.GetMatches(startDate, endDate);
                return Ok(matches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpGet("Live", Name = "GetLiveMatches")]
        [EndpointSummary("Gets the list of live football matches")]
        public async Task<ActionResult<SportradarResponse>> GetLiveMatches()
        {
            try
            {
                var liveMatches = await _sportradarService.GetLiveMatchesAsync();
                return Ok(liveMatches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpGet("{id}", Name = "GetMatchDetails")]
        [EndpointSummary("Gets match details")]
        public async Task<ActionResult<SportradarMatchDetailsResponse>> GetMatchDetails(int id)
        {
            try
            {
                var matchDetails = await _sportradarService.GetMatchDetailsById(id);
                return Ok(matchDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpGet("{id}/Statistics", Name = "GetMatchStatistics")]
        [EndpointSummary("Gets match detailed statistics")]
        public async Task<ActionResult<SportradarMatchStatisticsResponse>> GetMatchStatistics(int id)
        {
            try
            {
                var matchStatistics = await _sportradarService.GetMatchStatisticsById(id);
                return Ok(matchStatistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpGet("{id}/Head2head", Name = "GetH2hMatches")]
        [EndpointSummary("Gets head2head matches by matchId")]
        public async Task<ActionResult<SportradarHead2HeadResponse>> GetHead2HeadMatches(int id)
        {
            try
            {
                var h2hMatches = await _sportradarService.GetHead2HeadMatchesById(id);
                return Ok(h2hMatches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPost("{id}/Predict", Name = "GetMatchPredictionById")]
        [EndpointSummary("Gets match prediction by matchId")]
        public async Task<ActionResult<MatchPrediction>> PredictById(int id)
        {
            var matchPredictionData = await _matchService.GetMatchPredictionDataById(id);
            var prediction = _predictionService.Predict(matchPredictionData);
            return Ok(prediction);
        }

        [HttpPost("{id}/Prediction-data", Name = "GetPredictionDataById")]
        [EndpointSummary("Gets data prediction by matchId")]
        public async Task<ActionResult<MatchData>> PredictionDataById(int id)
        {
            var matchPredictionData = await _matchService.GetMatchPredictionDataById(id);
            return Ok(matchPredictionData);
        }

        [HttpPost("Predict", Name = "GetMatchPrediction")]
        [EndpointSummary("Gets match prediction")]
        public ActionResult<MatchPrediction> Predict([FromBody] MatchData matchData)
        {
            if (matchData == null)
                return BadRequest("Brak danych wejściowych!");

            var prediction = _predictionService.Predict(matchData);
            return Ok(prediction);
        }
    }
}