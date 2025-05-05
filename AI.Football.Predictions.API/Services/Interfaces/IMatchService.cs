using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.ML.Models;

namespace AI.Football.Predictions.API.Services.Interfaces
{
    public interface IMatchService
    {
        Task<MatchData> GetMatchPredictionDataById(int matchId); 
    }
}