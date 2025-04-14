using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.Integrations.Sportradar.Models;

namespace AI.Football.Predictions.Integrations.Sportradar.Services
{
    public interface ISportradarApiService
    {
        Task<SportradarResponse> GetLiveMatchesAsync();
        Task<SportradarResponse> GetMatches(DateTime startDate, DateTime endDate);
        Task<SportradarMatchDetailsResponse> GetMatchDetailsById(int gameId);
        Task<SportradarMatchStatisticsResponse> GetMatchStatisticsById(int gameId);
        Task<SportradarHead2HeadResponse> GetHead2HeadMatchesById(int gameId);
    }
}