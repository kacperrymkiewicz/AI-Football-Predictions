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
            // await _context.Database.ExecuteSqlRawAsync("TRUNCATE trainingdata");

            var endDate = DateTime.UtcNow;
            var startDate = endDate.AddYears(-1);
            for (var currentStartDate = startDate; currentStartDate < endDate; currentStartDate = currentStartDate.AddMonths(1))
            {
                var currentEndDate = currentStartDate.AddMonths(1).AddDays(-1);

                var matches = await _sportradarService.GetMatches(currentStartDate, currentEndDate);

                foreach (var match in matches.Games) 
                {
                    var historicalMatch = new HistoricalMatch 
                    {
                        MatchDate = match.StartTime,
                        HomeCompetitor = new Team
                        {
                            Name = match.HomeCompetitor.Name,
                            Score = (int) match.HomeCompetitor.Score,
                            IsWinner = match.HomeCompetitor.Score > match.AwayCompetitor.Score ? true : false
                        },
                        HomeStatistics = await CalculateRecentStatistics(match.Id, match.HomeCompetitor.Id),
                        AwayCompetitor = new Team
                        {
                            Name = match.AwayCompetitor.Name,
                            Score = (int) match.AwayCompetitor.Score,
                            IsWinner = match.AwayCompetitor.Score > match.HomeCompetitor.Score ? true : false
                        },
                        AwayStatistics = await CalculateRecentStatistics(match.Id, match.AwayCompetitor.Id),
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

        private async Task<TeamRecentStatistics> CalculateRecentStatistics(int matchId, int teamId)
        {
            var relevantMatches = await _sportradarService.GetHead2HeadMatchesById(matchId);
            bool isHomeCompetitor = relevantMatches.Game.HomeCompetitor.Id == teamId;
            List<RecentGame> recentGames = isHomeCompetitor ? relevantMatches.Game.HomeCompetitor.RecentGames : relevantMatches.Game.AwayCompetitor.RecentGames;

            int wins = 0, draws = 0, losses = 0;
            float totalGoals = 0;

            foreach (var recentGame in recentGames)
            {
                bool isHome = recentGame.HomeCompetitor.Id == teamId;
                bool isAway = recentGame.AwayCompetitor.Id == teamId;

                int teamScore = (int)(isHome ? recentGame.HomeCompetitor.Score : recentGame.AwayCompetitor.Score);
                int oppScore = (int)(isHome ?  recentGame.AwayCompetitor.Score : recentGame.HomeCompetitor.Score);

                totalGoals += teamScore;

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
                AvgShots = 0,
                AvgPossession = 0,
                AvgFouls = 0
            };
        }
    }
}