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

            var dateFrom = DateTime.UtcNow.ToString("dd'/'MM'/'yyyy");
            var dateTo = DateTime.UtcNow.AddDays(3).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            var response = await client.GetAsync($"web/games/myscores/?appTypeId=5&langId=35&timezoneName=Europe/Warsaw&competitions=572,11,25,7,156,153,8268,573,332&startDate={dateFrom}&endDate={dateTo}&showOdds=true&topBookmaker=151");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error fetching live matches");

            var content = await response.Content.ReadAsStringAsync();
            var deserializedResponse = JsonSerializer.Deserialize<SportradarResponse>(content, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

            return deserializedResponse!;
        }

        public async Task<SportradarMatchDetailsResponse> GetMatchDetailsById(int gameId) {
            var client = _httpClientFactory.CreateClient("Sportradar");

            var response = await client.GetAsync($"web/game/?appTypeId=5&langId=35&timezoneName=Europe/Warsaw&gameId={gameId}&topBookmaker=151");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error fetching match details");

            var content = await response.Content.ReadAsStringAsync();
            var deserializedResponse = JsonSerializer.Deserialize<SportradarMatchDetailsResponse>(content, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

            return deserializedResponse!;
        }

        public async Task<SportradarHead2HeadResponse> GetHead2HeadMatchesById(int gameId) {
            var client = _httpClientFactory.CreateClient("Sportradar");

            var response = await client.GetAsync($"web/games/h2h/?appTypeId=5&langId=35&timezoneName=Europe/Warsaw&gameId={gameId}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error fetching h2h matches");

            var content = await response.Content.ReadAsStringAsync();
            var deserializedResponse = JsonSerializer.Deserialize<SportradarHead2HeadResponse>(content, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

            return deserializedResponse!;
        }
    }
}