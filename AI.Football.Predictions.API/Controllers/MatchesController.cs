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

        // [HttpGet("Live", Name = "GetLiveMatches")]
        // [EndpointSummary("Gets the list of live football matches")]
        // public async Task<ActionResult<List<Match>>> GetLiveMatches()
        // {
        //     try
        //     {
        //         var liveMatches = await _footballDataService.GetLiveMatchesAsync();
        //         return Ok(liveMatches);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new { Message = ex.Message });
        //     }
        // }

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

        // [HttpGet("Live/Sportradar", Name = "GetLiveMatchesSportradar")]
        // [EndpointSummary("Gets the list of live football matches from Sportradar data")]
        // public async Task<ActionResult<SportradarResponse>> GetLiveMatchesSportradar()
        // {
        //     try
        //     {
        //         var liveMatches = await _sportradarService.GetLiveMatchesAsync();
        //         return Ok(liveMatches);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new { Message = ex.Message });
        //     }
        // }

    }
}