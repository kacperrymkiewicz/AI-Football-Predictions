using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Models;
using AI.Football.Predictions.API.Services.Interfaces;
using AI.Football.Predictions.Integrations.Sportradar.Services;
using AI.Football.Predictions.ML.Models;

namespace AI.Football.Predictions.API.Services
{
  public class MatchService : IMatchService
  {
    private readonly ISportradarApiService _sportradarService;
    private readonly MatchDataProcessor _matchDataProcessor;
    
    public MatchService(ISportradarApiService sportradarApiService, MatchDataProcessor matchDataProcessor)
    {
        _sportradarService = sportradarApiService;
        _matchDataProcessor = matchDataProcessor;
    }

    public async Task<MatchData> GetMatchPredictionDataById(int matchId)
    {
        var match = await _sportradarService.GetMatchDetailsById(matchId);
        var relevantMatches = await _sportradarService.GetHead2HeadMatchesById(matchId);
        var (h2hHomeWins, h2hAwayWins, h2hDraws) = _matchDataProcessor.CalculateHead2HeadStatistics(relevantMatches.Game.H2hGames, match.Game.HomeCompetitor.Id, match.Game.AwayCompetitor.Id);

        TeamRecentStatistics homeStatistics = await _matchDataProcessor.CalculateRecentStatistics(relevantMatches.Game.HomeCompetitor.RecentGames, match.Game.HomeCompetitor.Id);
        TeamRecentStatistics awayStatistics = await _matchDataProcessor.CalculateRecentStatistics(relevantMatches.Game.AwayCompetitor.RecentGames, match.Game.AwayCompetitor.Id);

        return new MatchData
        {
            HomeGoalsAvg = homeStatistics.AvgGoals,
            AwayGoalsAvg = awayStatistics.AvgGoals,
            HomePossessionAvg = 0,
            AwayPossessionAvg = 0,
            HomeShotsAvg = 0,
            AwayShotsAvg = 0,
            HomeWinRate = homeStatistics.WinRate,
            AwayWinRate = awayStatistics.WinRate,
            H2HHomeWins = h2hHomeWins,
            H2HAwayWins = h2hAwayWins,
            H2HDraws = h2hDraws
        };
    }
  }
}