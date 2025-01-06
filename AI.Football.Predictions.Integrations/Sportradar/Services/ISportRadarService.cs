using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.Integrations.Sportradar.Models;

namespace AI.Football.Predictions.Integrations.Sportradar.Services
{
    public interface ISportradarService
    {
        Task<SportradarResponse> GetLiveMatchesAsync();
    }
}