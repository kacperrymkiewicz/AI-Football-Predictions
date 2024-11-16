using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Football.Predictions.Integrations.FootballData.Services
{
    public class FootballDataService : IFootballDataService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FootballDataService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetLiveMatchesAsync()
        {
            var client = _httpClientFactory.CreateClient("FootballData");
            var response = await client.GetAsync("matches?status=LIVE");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error fetching live matches");

            return await response.Content.ReadAsStringAsync();
        }
    }
}