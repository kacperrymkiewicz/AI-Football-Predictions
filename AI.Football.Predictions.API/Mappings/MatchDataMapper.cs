using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Models;
using AI.Football.Predictions.ML.Models;

namespace AI.Football.Predictions.API.Mappings
{
    public static class MatchDataMapper
    {
        public static MatchData FromHistoricalMatch(HistoricalMatch m)
        {
            return new MatchData
            {
                HomeGoalsAvg = m.HomeStatistics.AvgGoals,
                AwayGoalsAvg = m.AwayStatistics.AvgGoals,
                HomePossessionAvg = m.HomeStatistics.AvgPossession,
                AwayPossessionAvg = m.AwayStatistics.AvgPossession,
                HomeShotsAvg = m.HomeStatistics.AvgShots,
                AwayShotsAvg = m.AwayStatistics.AvgShots,
                HomeAvgXG = m.HomeStatistics.AvgXG,
                AwayAvgXG = m.AwayStatistics.AvgXG,
                HomeAvgXGA = m.HomeStatistics.AvgXGA,
                AwayAvgXGA = m .AwayStatistics.AvgXGA,
                HomeWinRate = m.HomeStatistics.WinRate,
                AwayWinRate = m.AwayStatistics.WinRate,
                H2HHomeWins = m.H2HHomeWins,
                H2HAwayWins = m.H2HAwayWins,
                H2HDraws = m.H2HDraws,
                H2HHomeWinRate = m.H2HHomeWinRate,
                H2HAwayWinRate = m.H2HAwayWinRate,
                H2HDrawRate = m.H2HDrawRate,
                H2HHomeAvgXG = m.H2HHomeStatistics.AvgXG,
                H2HHomeAvgXGA = m.H2HHomeStatistics.AvgXGA,
                H2HAwayAvgXG = m.H2HAwayStatistics.AvgXG,
                H2HAwayAvgXGA = m.H2HAwayStatistics.AvgXGA,
                H2HHomeAvgBigChances = m.H2HHomeStatistics.AvgBigChances,
                H2HAwayAvgBigChances = m.H2HAwayStatistics.AvgBigChances,
                H2HHomeAvgCorners = m.H2HHomeStatistics.AvgCorners,
                H2HAwayAvgCorners = m.H2HAwayStatistics.AvgCorners,
                H2HHomeAvgFreeKicks = m.H2HHomeStatistics.AvgFreeKicks,
                H2HAwayAvgFreeKicks = m.H2HAwayStatistics.AvgFreeKicks,
                H2HHomeAvgRedCards = m.H2HHomeStatistics.AvgRedCards,
                H2HAwayAvgRedCards = m.H2HAwayStatistics.AvgRedCards,
                HomeScore = m.HomeCompetitor.Score,
                AwayScore = m.AwayCompetitor.Score,
                MatchResult = (uint)m.Result
            };
        }

        public static MatchData FromLiveMatchData(TeamRecentStatistics homeStatistics, TeamRecentStatistics awayStatistics, H2HProcessedData h2hData)
        {
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
                H2HHomeWins = h2hData.H2HHomeWins,
                H2HAwayWins = h2hData.H2HAwayWins,
                H2HDraws = h2hData.H2HDraws,
                H2HHomeAvgXG = h2hData.H2HHomeStatistics.AvgXG,
                H2HHomeAvgXGA = h2hData.H2HHomeStatistics.AvgXGA,
                H2HAwayAvgXG = h2hData.H2HAwayStatistics.AvgXG,
                H2HAwayAvgXGA = h2hData.H2HAwayStatistics.AvgXGA,
                H2HHomeAvgBigChances = h2hData.H2HHomeStatistics.AvgBigChances,
                H2HAwayAvgBigChances = h2hData.H2HAwayStatistics.AvgBigChances,
                H2HHomeAvgCorners = h2hData.H2HHomeStatistics.AvgCorners,
                H2HAwayAvgCorners = h2hData.H2HAwayStatistics.AvgCorners,
                H2HHomeAvgFreeKicks = h2hData.H2HHomeStatistics.AvgFreeKicks,
                H2HAwayAvgFreeKicks = h2hData.H2HAwayStatistics.AvgFreeKicks,
                H2HHomeAvgRedCards = h2hData.H2HHomeStatistics.AvgRedCards,
                H2HAwayAvgRedCards = h2hData.H2HAwayStatistics.AvgRedCards,
                H2HHomeWinRate = h2hData.H2HHomeWins == 0 ? 0 : (float) h2hData.H2HHomeWins / (h2hData.H2HHomeWins + h2hData.H2HAwayWins + h2hData.H2HDraws),
                H2HAwayWinRate = h2hData.H2HAwayWins == 0 ? 0 : (float) h2hData.H2HAwayWins / (h2hData.H2HHomeWins + h2hData.H2HAwayWins + h2hData.H2HDraws),
                H2HDrawRate = h2hData.H2HDraws == 0 ? 0 : (float) h2hData.H2HDraws / (h2hData.H2HHomeWins + h2hData.H2HAwayWins + h2hData.H2HDraws),
            };
        }
    }
}