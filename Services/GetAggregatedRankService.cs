using Apps_Review_Api.Models;
using Apps_Review_Api.Repositories;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Apps_Review_Api.Services
{
    public class GetAggregatedRankService
    {
        private readonly IConfiguration _config;
        private readonly GenreRepository _genresServices;

        public GetAggregatedRankService(IConfiguration configuration)
        {
            this._config = configuration;
        }

        public async Task<List<AggregatedRankModel>> getAggregatedRanks(AppsReqBinding appsReqBinding)
        {
            using (var client = new HttpClient())
            {
                string baseUrl = $"https://api.appmonsta.com/v1/stores/{appsReqBinding.store}/rankings/aggregate.json";

                string country = appsReqBinding.country;
                string dateTime = appsReqBinding.date;

                string username = _config.GetSection("AppSettings:Api_Key").Value;
                string password = _config.GetSection("AppSettings:Password_Key").Value;
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

                string endpoint = $"api/endpoint?country={country}&date={dateTime}";
                HttpResponseMessage response = await client.GetAsync(baseUrl + endpoint);

                if (response.IsSuccessStatusCode)
                {
                    List<AggregatedRankModel> aggregatedRanks = new List<AggregatedRankModel>();
                    var data = response.Content.ReadAsStringAsync().Result;

                    foreach (string line in data.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                    {
                        AggregatedRankModel aggregatedRank = JsonSerializer.Deserialize<AggregatedRankModel>(line);
                        aggregatedRanks.Add(aggregatedRank);
                    }
                    return aggregatedRanks;
                }
                else
                {
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                    return null;
                }
            }
        }
    }
}
