using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly ILogger<MatchDataProcessor> _logger;
        private readonly ISportradarApiService _sportradarService;

        public MatchDataProcessor(DataContext context, ISportradarApiService sportradarService, ILogger<MatchDataProcessor> logger)
        {
            _context = context;
            _logger = logger;
            _sportradarService = sportradarService;
        }

        public async Task ProcessAndSaveMatchDataAsync()
        {
            var trainingDataList = new List<HistoricalMatch>();

            var endDate = DateTime.UtcNow;
            var startDate = endDate.AddYears(-3);
            for (var currentStartDate = startDate; currentStartDate < endDate; currentStartDate = currentStartDate.AddMonths(1))
            {
                var currentEndDate = currentStartDate.AddMonths(1).AddDays(-1);

                var matches = await _sportradarService.GetMatches(currentStartDate, currentEndDate);
                if(matches.Games == null)
                    continue;

                foreach (var match in matches.Games)
                {
                    try
                    {
                        var relevantMatches = await _sportradarService.GetHead2HeadMatchesById(match.Id);
                        var h2hProcessedData = await CalculateHead2HeadStatistics(relevantMatches.Game.H2hGames, match.HomeCompetitor.Id, match.AwayCompetitor.Id);

                        var historicalMatch = new HistoricalMatch
                        {
                            MatchDate = match.StartTime,
                            MatchId = match.Id,
                            HomeCompetitor = new Team
                            {
                                Name = match.HomeCompetitor.Name,
                                Score = (int)match.HomeCompetitor.Score,
                                IsWinner = match.HomeCompetitor.Score > match.AwayCompetitor.Score
                            },
                            HomeStatistics = await CalculateRecentStatistics(relevantMatches.Game.HomeCompetitor.RecentGames, match.HomeCompetitor.Id),
                            AwayCompetitor = new Team
                            {
                                Name = match.AwayCompetitor.Name,
                                Score = (int)match.AwayCompetitor.Score,
                                IsWinner = match.AwayCompetitor.Score > match.HomeCompetitor.Score
                            },
                            AwayStatistics = await CalculateRecentStatistics(relevantMatches.Game.AwayCompetitor.RecentGames, match.AwayCompetitor.Id),
                            H2HHomeWins = h2hProcessedData.H2HHomeWins,
                            H2HAwayWins = h2hProcessedData.H2HAwayWins,
                            H2HDraws = h2hProcessedData.H2HDraws,

                            H2HHomeStatistics = h2hProcessedData.H2HHomeStatistics,
                            H2HAwayStatistics = h2hProcessedData.H2HAwayStatistics,

                            Result = MatchResultHelper.GetResult((int)match.HomeCompetitor.Score, (int)match.AwayCompetitor.Score)
                        };

                        trainingDataList.Add(historicalMatch);
                        _logger.LogInformation("✅ Successfully processed match {MatchId}: {HistoricalMatch}", match.Id, historicalMatch.ToString());
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning("⚠️ Omitted match {MatchId} due to an fetching error: {Message}", match.Id, ex.Message);
                    }
                };
            }

            await _context.HistoricalMatches.AddRangeAsync(trainingDataList);
            await _context.SaveChangesAsync();
        }

        public async Task<TeamRecentStatistics> CalculateRecentStatistics(List<RecentGame> recentGames, int teamId)
        {
            Dictionary<string, (float sum, int count)> processedStatistics = InitEmptyStatDict();
            int wins = 0, draws = 0, losses = 0, processedGames = 0;
            float totalGoals = 0;

            foreach (var recentGame in recentGames.Take(8))
            {
                var matchStatistics = await _sportradarService.GetMatchStatisticsById(recentGame.Id);
                bool isHome = recentGame.HomeCompetitor.Id == teamId;
                bool isAway = recentGame.AwayCompetitor.Id == teamId;

                int teamScore = (int)(isHome ? recentGame.HomeCompetitor.Score : recentGame.AwayCompetitor.Score);
                int oppScore = (int)(isHome ?  recentGame.AwayCompetitor.Score : recentGame.HomeCompetitor.Score);

                totalGoals += teamScore;
                processedGames++;

                var singleGameStats = ProcessMatchStatistics(matchStatistics.Statistics, teamId);
                AggregateStats(processedStatistics, singleGameStats);

                if (teamScore > oppScore) wins++;
                else if (teamScore == oppScore) draws++;
                else losses++;
            }

            return new TeamRecentStatistics
            {
                LastMatchesPlayed = processedGames,
                Wins = wins,
                Draws = draws,
                Losses = losses,
                AvgGoals = SafeAverage(totalGoals, processedGames),
                AvgShots = SafeAverage(processedStatistics["Shots"].sum, processedStatistics["Shots"].count),
                AvgPossession = SafeAverage(processedStatistics["Possession"].sum, processedStatistics["Possession"].count),
                AvgFouls = SafeAverage(processedStatistics["Fouls"].sum, processedStatistics["Fouls"].count),
                AvgXG = SafeAverage(processedStatistics["xG"].sum, processedStatistics["xG"].count),
                AvgXGA = SafeAverage(processedStatistics["xGA"].sum, processedStatistics["xGA"].count),
            };
        }

        public async Task<H2HProcessedData> CalculateHead2HeadStatistics(IEnumerable<H2hGame> h2hMatches, int homeTeamId, int awayTeamId)
        {
            Dictionary<string, (float sum, int count)> homeProcessedStatistics = InitEmptyStatDict();
            Dictionary<string, (float sum, int count)> awayProcessedStatistics = InitEmptyStatDict();
            int h2hHomeWins = 0, h2hAwayWins = 0, h2hDraws = 0, processedGames = 0;
            float totalHomeGoals = 0, totalAwayGoals = 0;

            foreach (var match in h2hMatches.Take(15))
            {
                bool isHome = match.HomeCompetitor.Id == homeTeamId;
                bool isAway = match.AwayCompetitor.Id == awayTeamId;

                var matchStatistics = await _sportradarService.GetMatchStatisticsById(match.Id);

                var singleHomeStats = ProcessMatchStatistics(matchStatistics.Statistics, homeTeamId);
                var singleAwayStats = ProcessMatchStatistics(matchStatistics.Statistics, awayTeamId);
                AggregateStats(homeProcessedStatistics, singleHomeStats);
                AggregateStats(awayProcessedStatistics, singleAwayStats);

                int homeScore = (int)(isHome ? match.HomeCompetitor.Score : match.AwayCompetitor.Score);
                int awayScore = (int)(isHome ?  match.AwayCompetitor.Score : match.HomeCompetitor.Score);
                totalHomeGoals += homeScore;
                totalAwayGoals += awayScore;
                processedGames++;

                if ((isHome && match.HomeCompetitor.Score > match.AwayCompetitor.Score) || (!isHome && match.HomeCompetitor.Score < match.AwayCompetitor.Score))
                {
                    h2hHomeWins++;
                }
                else if ((isAway && match.AwayCompetitor.Score > match.HomeCompetitor.Score) || (!isAway && match.AwayCompetitor.Score < match.HomeCompetitor.Score))
                {
                    h2hAwayWins++;
                }
                else if (match.HomeCompetitor.Score == match.AwayCompetitor.Score)
                {
                    h2hDraws++;
                }
            }

            H2HRecentStatistics h2hHomeStatistics = CreateH2HStatistics(homeProcessedStatistics, SafeAverage(totalHomeGoals, processedGames));
            H2HRecentStatistics h2hAwayStatistics = CreateH2HStatistics(awayProcessedStatistics, SafeAverage(totalAwayGoals, processedGames));

            return new H2HProcessedData
            {
                H2HHomeWins = h2hHomeWins,
                H2HAwayWins = h2hAwayWins,
                H2HDraws = h2hDraws,
                H2HHomeStatistics = h2hHomeStatistics,
                H2HAwayStatistics = h2hAwayStatistics,
            };
        }

        private Dictionary<string, (float sum, int count)> ProcessMatchStatistics(IEnumerable<Statistic> statistics, int teamId)
        {
            var statDict = InitEmptyStatDict();

            if (statistics == null) return statDict;
            
            ProcessSingleStat(statistics, teamId, 10, "Possession", statDict, s => (float)s.ValuePercentage);
            ProcessSingleStat(statistics, teamId, 3, "Shots", statDict, s => float.TryParse(s.Value, out var result) ? result : 0f);
            ProcessSingleStat(statistics, teamId, 12, "Fouls", statDict, s => float.TryParse(s.Value, out var result) ? result : 0f);
            ProcessSingleStat(statistics, teamId, 76, "xG", statDict, s => float.TryParse(s.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result) ? result : 0f);
            ProcessSingleStat(statistics, teamId, 77, "xGA", statDict, s => float.TryParse(s.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result) ? result : 0f);
            ProcessSingleStat(statistics, teamId, 24, "BigChances", statDict, s => float.TryParse(s.Value, out var result) ? result : 0f);
            ProcessSingleStat(statistics, teamId, 4, "ShotsOnTarget", statDict, s => float.TryParse(s.Value, out var result) ? result : 0f);
            ProcessSingleStat(statistics, teamId, 8, "Corners", statDict, s => float.TryParse(s.Value, out var result) ? result : 0f);
            ProcessSingleStat(statistics, teamId, 13, "FreeKicks", statDict, s => float.TryParse(s.Value, out var result) ? result : 0f);
            ProcessSingleStat(statistics, teamId, 2, "RedCards", statDict, s => float.TryParse(s.Value, out var result) ? result : 0f);

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

        private H2HRecentStatistics CreateH2HStatistics(Dictionary<string, (float sum, int count)> stats, float avgGoals)
        {
            return new H2HRecentStatistics
            {
                AvgGoals = avgGoals,
                AvgShots = SafeAverage(stats["Shots"].sum, stats["Shots"].count),
                AvgPossession = SafeAverage(stats["Possession"].sum, stats["Possession"].count),
                AvgFouls = SafeAverage(stats["Fouls"].sum, stats["Fouls"].count),
                AvgXG = SafeAverage(stats["xG"].sum, stats["xG"].count),
                AvgXGA = SafeAverage(stats["xGA"].sum, stats["xGA"].count),
                AvgBigChances = SafeAverage(stats["BigChances"].sum, stats["BigChances"].count),
                AvgShotsOnTarget = SafeAverage(stats["ShotsOnTarget"].sum, stats["ShotsOnTarget"].count),
                AvgCorners = SafeAverage(stats["Corners"].sum, stats["Corners"].count),
                AvgFreeKicks = SafeAverage(stats["FreeKicks"].sum, stats["FreeKicks"].count),
                AvgRedCards = SafeAverage(stats["RedCards"].sum, stats["RedCards"].count)
            };
        }

        private void AggregateStats(Dictionary<string, (float sum, int count)> target, Dictionary<string, (float sum, int count)> source)
        {
            foreach (var key in source.Keys)
            {
                var (srcSum, srcCount) = source[key];
                var (tgtSum, tgtCount) = target[key];
                target[key] = (tgtSum + srcSum, tgtCount + srcCount);
            }
        }

        private Dictionary<string, (float sum, int count)> InitEmptyStatDict()
        {
            return new Dictionary<string, (float sum, int count)>
            {
                { "Possession", (0, 0) },
                { "Shots", (0, 0) },
                { "Fouls", (0, 0) },
                { "xG", (0, 0) },
                { "xGA", (0, 0) },
                { "BigChances", (0, 0) },
                { "ShotsOnTarget", (0, 0) },
                { "Corners", (0, 0) },
                { "FreeKicks", (0, 0) },
                { "RedCards", (0, 0) }
            };
        }

        private float SafeAverage(float sum, int count)
        {
            if (count == 0) return 0;
            float result = sum / count;
            return float.IsNaN(result) || float.IsInfinity(result) ? 0 : result;
        }
    }
}