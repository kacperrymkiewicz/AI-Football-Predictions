using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AI.Football.Predictions.Integrations.FootballData.Models;

namespace AI.Football.Predictions.Integrations.FootballData.Services
{
    public class FootballDataService : IFootballDataService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FootballDataService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Match>> GetLiveMatchesAsync()
        {
            var client = _httpClientFactory.CreateClient("FootballData");

            var dateFrom = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var dateTo = DateTime.UtcNow.AddDays(10).ToString("yyyy-MM-dd");

            var response = await client.GetAsync($"matches?dateFrom={dateFrom}&dateTo={dateTo}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error fetching live matches");

            var content = await response.Content.ReadAsStringAsync();
            var deserializedResponse = JsonSerializer.Deserialize<FootballDataResponse>(content, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

            return deserializedResponse?.Matches.Take(10).ToList() ?? new List<Match>();
        }
    }
}