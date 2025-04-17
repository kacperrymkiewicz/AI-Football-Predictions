using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Data;
using AI.Football.Predictions.API.Models;
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
            var trainingDataList = new List<MatchTrainingData>();
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE trainingdata");

            var endDate = DateTime.UtcNow;
            var startDate = endDate.AddYears(-1);
            for (var currentStartDate = startDate; currentStartDate < endDate; currentStartDate = currentStartDate.AddMonths(1))
            {
                var currentEndDate = currentStartDate.AddMonths(1).AddDays(-1);

                var matches = await _sportradarService.GetMatches(currentStartDate, currentEndDate);

                matches.Games.ForEach(match => 
                {
                    var trainingData = new MatchTrainingData 
                    {
                        HomeGoals = (int) match.HomeCompetitor.Score,
                        AwayGoals = (int) match.AwayCompetitor.Score,
                        HomePossession = 0,
                        AwayPossession = 0,
                        MatchResult = (uint)(match.HomeCompetitor.Score > match.AwayCompetitor.Score ? 0 : (match.HomeCompetitor.Score < match.AwayCompetitor.Score ? 1 : 2))
                    };

                    trainingDataList.Add(trainingData);
                    Console.WriteLine(trainingData.ToString());
                });
            }

            await _context.TrainingData.AddRangeAsync(trainingDataList);
            await _context.SaveChangesAsync();
        }
    }
}