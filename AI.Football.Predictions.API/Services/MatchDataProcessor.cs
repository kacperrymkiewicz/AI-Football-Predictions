using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Data;
using AI.Football.Predictions.API.Helpers;
using AI.Football.Predictions.API.Models;
using AI.Football.Predictions.Integrations.FootballData.Models;
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

                matches.Games.ForEach(match => 
                {
                    var historicalMatch = new HistoricalMatch 
                    {
                        MatchDate = match.StartTime,
                        HomeCompetitor = new Team
                        {
                            Name = match.HomeCompetitor.Name,
                            Score = (int) match.HomeCompetitor.Score,
                            IsWinner = match.HomeCompetitor.Score > match.AwayCompetitor.Score ? true : false,
                            Statistics = new TeamStatistics
                            {
                                AvgGoals = 0.0f,
                                ShotsPerGame = 0.0f,
                                BallPossession = 0.0f,
                                Fouls = 0.0f,      
                            }
                        },
                        AwayCompetitor = new Team
                        {
                            Name = match.AwayCompetitor.Name,
                            Score = (int) match.AwayCompetitor.Score,
                            IsWinner = match.AwayCompetitor.Score > match.HomeCompetitor.Score ? true : false,
                            Statistics = new TeamStatistics
                            {
                                AvgGoals = 0.0f,
                                ShotsPerGame = 0.0f,
                                BallPossession = 0.0f,
                                Fouls = 0.0f,
                            }
                        },
                        Result = MatchResultHelper.GetResult((int) match.HomeCompetitor.Score, (int) match.AwayCompetitor.Score)
                    };

                    trainingDataList.Add(historicalMatch);
                    Console.WriteLine(historicalMatch.ToString());
                });
            }

            await _context.HistoricalMatches.AddRangeAsync(trainingDataList);
            await _context.SaveChangesAsync();
        }
    }
}