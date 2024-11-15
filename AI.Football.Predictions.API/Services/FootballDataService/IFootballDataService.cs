using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Models;

namespace AI.Football.Predictions.API.Services.FootballDataService
{
    public interface IFootballDataService
    {
        Task<ServiceResponse<string>> GetLiveMatchesAsync();
    }
}