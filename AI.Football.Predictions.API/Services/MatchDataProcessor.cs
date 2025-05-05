using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Data;
using AI.Football.Predictions.API.Helpers;
using AI.Football.Predictions.API.Models;
using AI.Football.Predictions.Integrations.FootballData.Models;
using AI.Football.Predictions.Integrations.Sportradar.Models;
using AI.Football.Predictions.Integrations.Sportradar.Services;
using Microsoft.EntityFrameworkCore;

namespace AI.Football.Predictions.API.Services
{
    public class MatchDataProcessor
    {
        private readonly DataContext _context;
        private readonly ISportradarApiService _sportradarService;

        public MatchDataProcessor(DataContext context, ISportradarApiService sportradarService)
        {
            _context = context;
            _sportradarService = sportradarService;
        }

        public async Task ProcessAndSaveMatchDataAsync()
        {
            var trainingDataList = new List<HistoricalMatch>();

            var endDate = DateTime.UtcNow;
            var startDate = endDate.AddYears(-1);
            for (var currentStartDate = startDate; currentStartDate < endDate; currentStartDate = currentStartDate.AddMonths(1))
            {
                var currentEndDate = currentStartDate.AddMonths(1).AddDays(-1);

                var matches = await _sportradarService.GetMatches(currentStartDate, currentEndDate);
                if(matches.Games == null)
                    continue;

                foreach (var match in matches.Games) 
                {
                    var relevantMatches = await _sportradarService.GetHead2HeadMatchesById(match.Id);
                    var historicalMatch = new HistoricalMatch 
                    {
                        MatchDate = match.StartTime,
                        HomeCompetitor = new Team
                        {
                            Name = match.HomeCompetitor.Name,
                            Score = (int) match.HomeCompetitor.Score,
                            IsWinner = match.HomeCompetitor.Score > match.AwayCompetitor.Score
                        },
                        HomeStatistics = await CalculateRecentStatistics(relevantMatches.Game.HomeCompetitor.RecentGames, match.HomeCompetitor.Id),
                        AwayCompetitor = new Team
                        {
                            Name = match.AwayCompetitor.Name,
                            Score = (int) match.AwayCompetitor.Score,
                            IsWinner = match.AwayCompetitor.Score > match.HomeCompetitor.Score
                        },
                        AwayStatistics = await CalculateRecentStatistics(relevantMatches.Game.AwayCompetitor.RecentGames, match.AwayCompetitor.Id),
                        H2HHomeWins = 0,
                        H2HAwayWins = 0,
                        H2HDraws = 0,

                        Result = MatchResultHelper.GetResult((int) match.HomeCompetitor.Score, (int) match.AwayCompetitor.Score)
                    };

                    trainingDataList.Add(historicalMatch);
                    Console.WriteLine(historicalMatch.ToString());
                };
            }

            await _context.HistoricalMatches.AddRangeAsync(trainingDataList);
            await _context.SaveChangesAsync();
        }

        private async Task<TeamRecentStatistics> CalculateRecentStatistics(List<RecentGame> recentGames, int teamId)
        {
            Dictionary<string, (float sum, int count)> processedStatistics = [];
            int wins = 0, draws = 0, losses = 0;
            float totalGoals = 0;

            foreach (var recentGame in recentGames.Take(10))
            {
                var matchStatistics = await _sportradarService.GetMatchStatisticsById(recentGame.Id);
                bool isHome = recentGame.HomeCompetitor.Id == teamId;
                bool isAway = recentGame.AwayCompetitor.Id == teamId;

                int teamScore = (int)(isHome ? recentGame.HomeCompetitor.Score : recentGame.AwayCompetitor.Score);
                int oppScore = (int)(isHome ?  recentGame.AwayCompetitor.Score : recentGame.HomeCompetitor.Score);

                totalGoals += teamScore;

                processedStatistics = ProcessMatchStatistics(matchStatistics.Statistics, teamId);

                if (teamScore > oppScore) wins++;
                else if (teamScore == oppScore) draws++;
                else losses++;
            }

            return new TeamRecentStatistics
            {
                LastMatchesPlayed = recentGames.Count,
                Wins = wins,
                Draws = draws,
                Losses = losses,
                AvgGoals = recentGames.Count == 0 ? 0 : totalGoals / recentGames.Count,
                AvgShots = processedStatistics["Shots"].count == 0 ? 0 : processedStatistics["Shots"].sum / processedStatistics["Shots"].count,
                AvgPossession = processedStatistics["Possession"].count == 0 ? 0 : processedStatistics["Possession"].sum / processedStatistics["Possession"].count,
                AvgFouls = processedStatistics["Fouls"].count == 0 ? 0 : processedStatistics["Fouls"].sum / processedStatistics["Fouls"].count
            };
        }

        private Dictionary<string, (float sum, int count)> ProcessMatchStatistics(IEnumerable<Statistic> statistics, int teamId)
        {
            var statDict = new Dictionary<string, (float sum, int count)>
            {
                { "Possession", (0, 0) },
                { "Shots", (0, 0) },
                { "Fouls", (0, 0) }
            };

            if (statistics == null) return statDict;
            
            ProcessSingleStat(statistics, teamId, 10, "Possession", statDict, s => (float)s.ValuePercentage);
            ProcessSingleStat(statistics, teamId, 3, "Shots", statDict, s => float.TryParse(s.Value, out var result) ? result : 0f);
            ProcessSingleStat(statistics, teamId, 12, "Fouls", statDict, s => float.TryParse(s.Value, out var result) ? result : 0f);

            return statDict;
        }

        private void ProcessSingleStat(IEnumerable<Statistic> statistics, int teamId, int statId, string statKey, Dictionary<string, (float sum, int count)> statDict, Func<Statistic, float> valueExtractor)
        {
            var stat = statistics.FirstOrDefault(s => s.Id == statId && s.CompetitorId == teamId);
            if (stat != null)
            {
                var extractedValue = valueExtractor(stat);
                statDict[statKey] = (statDict[statKey].sum + extractedValue, statDict[statKey].count + 1);
            }
        }
    }
}