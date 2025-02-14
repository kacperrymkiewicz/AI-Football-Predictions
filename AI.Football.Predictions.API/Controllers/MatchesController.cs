using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.Integrations.FootballData.Models;
using AI.Football.Predictions.Integrations.FootballData.Services;
using AI.Football.Predictions.Integrations.Sportradar.Models;
using AI.Football.Predictions.Integrations.Sportradar.Services;
using Microsoft.AspNetCore.Mvc;

namespace AI.Football.Predictions.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly IFootballDataService _footballDataService;
        private readonly ISportradarService _sportradarService;

        public MatchesController(IFootballDataService footballDataService, ISportradarService sportradarService)
        {
            _footballDataService = footballDataService;
            _sportradarService = sportradarService;
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
    }
}