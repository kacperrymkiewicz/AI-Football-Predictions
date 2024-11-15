using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Services.FootballDataService;
using Microsoft.AspNetCore.Mvc;

namespace AI.Football.Predictions.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly IFootballDataService _footballDataService;

        public MatchesController(IFootballDataService footballDataService)
        {
            _footballDataService = footballDataService;
        }

        [HttpGet("Live")]
        public async Task<IActionResult> GetLiveMatches()
        {
            try
            {
                var liveMatches = await _footballDataService.GetLiveMatchesAsync();
                return Ok(liveMatches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

    }
}