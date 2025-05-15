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
        var h2hProcessedData = await _matchDataProcessor.CalculateHead2HeadStatistics(relevantMatches.Game.H2hGames, match.Game.HomeCompetitor.Id, match.Game.AwayCompetitor.Id);

        TeamRecentStatistics homeStatistics = await _matchDataProcessor.CalculateRecentStatistics(relevantMatches.Game.HomeCompetitor.RecentGames, match.Game.HomeCompetitor.Id);
        TeamRecentStatistics awayStatistics = await _matchDataProcessor.CalculateRecentStatistics(relevantMatches.Game.AwayCompetitor.RecentGames, match.Game.AwayCompetitor.Id);

        return new MatchData
        {
            HomeGoalsAvg = homeStatistics.AvgGoals,
            AwayGoalsAvg = awayStatistics.AvgGoals,
            HomePossessionAvg = homeStatistics.AvgPossession,
            AwayPossessionAvg = awayStatistics.AvgPossession,
            HomeShotsAvg = homeStatistics.AvgShots,
            AwayShotsAvg = awayStatistics.AvgShots,
            HomeAvgXG = homeStatistics.AvgXG,
            AwayAvgXG = awayStatistics.AvgXG,
            HomeAvgXGA = homeStatistics.AvgXGA,
            AwayAvgXGA = awayStatistics.AvgXGA,
            HomeWinRate = homeStatistics.WinRate,
            AwayWinRate = awayStatistics.WinRate,
            H2HHomeWins = h2hProcessedData.H2HHomeWins,
            H2HAwayWins = h2hProcessedData.H2HAwayWins,
            H2HDraws = h2hProcessedData.H2HDraws,
            H2HHomeAvgXG = h2hProcessedData.H2HHomeStatistics.AvgXG,
            H2HHomeAvgXGA = h2hProcessedData.H2HHomeStatistics.AvgXGA,
            H2HAwayAvgXG = h2hProcessedData.H2HAwayStatistics.AvgXG,
            H2HAwayAvgXGA = h2hProcessedData.H2HAwayStatistics.AvgXGA,
            H2HHomeAvgBigChances = h2hProcessedData.H2HHomeStatistics.AvgBigChances,
            H2HAwayAvgBigChances = h2hProcessedData.H2HAwayStatistics.AvgBigChances,
            H2HHomeAvgCorners = h2hProcessedData.H2HHomeStatistics.AvgCorners,
            H2HAwayAvgCorners = h2hProcessedData.H2HAwayStatistics.AvgCorners,
            H2HHomeAvgFreeKicks = h2hProcessedData.H2HHomeStatistics.AvgFreeKicks,
            H2HAwayAvgFreeKicks = h2hProcessedData.H2HAwayStatistics.AvgFreeKicks,
            H2HHomeAvgRedCards = h2hProcessedData.H2HHomeStatistics.AvgRedCards,
            H2HAwayAvgRedCards = h2hProcessedData.H2HAwayStatistics.AvgRedCards,
            H2HHomeWinRate = h2hProcessedData.H2HHomeWins == 0 ? 0 : (float) h2hProcessedData.H2HHomeWins / (h2hProcessedData.H2HHomeWins + h2hProcessedData.H2HAwayWins + h2hProcessedData.H2HDraws),
            H2HAwayWinRate = h2hProcessedData.H2HAwayWins == 0 ? 0 : (float) h2hProcessedData.H2HAwayWins / (h2hProcessedData.H2HHomeWins + h2hProcessedData.H2HAwayWins + h2hProcessedData.H2HDraws),
            H2HDrawRate = h2hProcessedData.H2HDraws == 0 ? 0 : (float) h2hProcessedData.H2HDraws / (h2hProcessedData.H2HHomeWins + h2hProcessedData.H2HAwayWins + h2hProcessedData.H2HDraws),
        };
    }
  }
}