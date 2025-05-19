using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AI.Football.Predictions.Integrations.Sportradar.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace AI.Football.Predictions.Integrations.Sportradar.Services
{
    public class SportradarApiService : ISportradarApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;

        public SportradarApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _client = httpClientFactory.CreateClient("Sportradar");
        }

        public async Task<SportradarResponse> GetMatches(DateTime startDate, DateTime endDate)
        {
            var dateFrom = startDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var dateTo = endDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            var queryParams = new Dictionary<string, string?>
            {
                ["appTypeId"] = "5",
                ["langId"] = "35",
                ["timezoneName"] = "Europe/Warsaw",
                ["competitions"] = "572,11,25,7,156,153,8268,573,332,17,35",
                ["startDate"] = dateFrom,
                ["endDate"] = dateTo,
                ["showOdds"] = "true",
                ["topBookmaker"] = "151"
            };

            var url = QueryHelpers.AddQueryString("web/games/myscores/", queryParams);
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error fetching matches");

            var content = await response.Content.ReadAsStringAsync();
            var deserializedResponse = JsonSerializer.Deserialize<SportradarResponse>(content, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

            return deserializedResponse!;
        }

        public async Task<SportradarResponse> GetLiveMatchesAsync()
        {
            var client = _httpClientFactory.CreateClient("Sportradar");

            var dateFrom = DateTime.UtcNow.ToString("dd'/'MM'/'yyyy");
            var dateTo = DateTime.UtcNow.AddDays(3).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            var response = await client.GetAsync($"web/games/myscores/?appTypeId=5&langId=35&timezoneName=Europe/Warsaw&competitions=572,11,25,7,156,153,8268,573,332,17,35&startDate={dateFrom}&endDate={dateTo}&showOdds=true&topBookmaker=151");

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

        public async Task<SportradarMatchStatisticsResponse> GetMatchStatisticsById(int gameId) {
            var client = _httpClientFactory.CreateClient("Sportradar");

            var response = await client.GetAsync($"web/game/stats/?appTypeId=5&langId=35&timezoneName=Europe/Warsaw&games={gameId}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error fetching match statistics");

            var content = await response.Content.ReadAsStringAsync();
            var deserializedResponse = JsonSerializer.Deserialize<SportradarMatchStatisticsResponse>(content, new JsonSerializerOptions {
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