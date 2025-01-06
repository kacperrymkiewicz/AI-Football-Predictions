using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AI.Football.Predictions.Integrations.Sportradar.Models;

namespace AI.Football.Predictions.Integrations.Sportradar.Services
{
    public class SportradarService : ISportradarService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SportradarService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<SportradarResponse> GetLiveMatchesAsync()
        {
            var client = _httpClientFactory.CreateClient("Sportradar");

            var dateFrom = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var dateTo = DateTime.UtcNow.AddDays(10).ToString("yyyy-MM-dd");

            var response = await client.GetAsync($"/web/game/stats/?appTypeId=5&langId=35&timezoneName=Europe/Warsaw&userCountryId=37&games=3877903&lastUpdateId=4996686957");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error fetching live matches");

            var content = await response.Content.ReadAsStringAsync();
            var deserializedResponse = JsonSerializer.Deserialize<SportradarResponse>(content, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

            return deserializedResponse;
        }
    }
}