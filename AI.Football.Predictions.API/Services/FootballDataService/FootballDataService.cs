using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Models;

namespace AI.Football.Predictions.API.Services.FootballDataService
{
    public class FootballDataService : IFootballDataService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FootballDataService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ServiceResponse<string>> GetLiveMatchesAsync()
        {
            var client = _httpClientFactory.CreateClient("FootballData");
            var response = await client.GetAsync("matches?status=LIVE");
            
            if (!response.IsSuccessStatusCode)
                throw new Exception("Error fetching live matches");

            return ServiceResponse<string>.SuccessResponse(await response.Content.ReadAsStringAsync());
        }
    }
}