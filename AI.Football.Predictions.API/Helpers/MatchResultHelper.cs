using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Models;

namespace AI.Football.Predictions.API.Helpers
{
    public static class MatchResultHelper
    {
        public static Result GetResult(int homeScore, int awayScore)
        {
            if (homeScore > awayScore)
                return Result.Home;
            if (homeScore < awayScore)
                return Result.Away;

            return Result.Draw;       
        }
    }
}