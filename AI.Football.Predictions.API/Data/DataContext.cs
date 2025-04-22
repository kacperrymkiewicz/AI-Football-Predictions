using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AI.Football.Predictions.API.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("DatabaseConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<MatchTrainingData> TrainingData => Set<MatchTrainingData>();
        public DbSet<HistoricalMatch> HistoricalMatches => Set<HistoricalMatch>();
    }
}