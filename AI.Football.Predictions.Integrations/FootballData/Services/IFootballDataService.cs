using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.Integrations.FootballData.Models;

namespace AI.Football.Predictions.Integrations.FootballData.Services
{
    public interface IFootballDataService
    {
        Task<List<Match>> GetLiveMatchesAsync();
    }
}