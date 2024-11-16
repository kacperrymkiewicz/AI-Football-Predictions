using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.FootballData.Services
{
    public interface IFootballDataService
    {
        Task<string> GetLiveMatchesAsync();
    }
}