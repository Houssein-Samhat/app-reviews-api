using Apps_Review_Api.Interface;
using Apps_Review_Api.Models;
using Apps_Review_Api.Services;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Apps_Review_Api.Repositories
{
    public class GenreRepository : IGenre
    {
        private readonly IConfiguration _config;
        private readonly GetAggregatedRankService _aggregatedRankService;
        private readonly SingleAppDetailsRepository _singleAppDetailsService;

        public GenreRepository(IConfiguration configuration, GetAggregatedRankService getAggregatedRankService, SingleAppDetailsRepository singleAppDetailsService)
        {
            _config = configuration;
            _aggregatedRankService = getAggregatedRankService;
            _singleAppDetailsService = singleAppDetailsService;
        }

        public async Task<List<Genre>> GetAllGenresAsync(GenreReqBinding detail)
        {

            using (var client = new HttpClient())
            {
                string baseUrl = $"https://api.appmonsta.com/v1/stores/{detail.store}/rankings/genres.json";

                string country = detail.country;

                string dateTime = detail.date;

                string username = _config.GetSection("AppSettings:Api_Key").Value;
                string password = _config.GetSection("AppSettings:Password_Key").Value;

                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

                string endpoint = $"api/endpoint?date={dateTime}";
                HttpResponseMessage response = await client.GetAsync(baseUrl + endpoint);

                if (response.IsSuccessStatusCode)
                {
                    List<GenreRanking> genreRanks = new List<GenreRanking>();
                    var data = response.Content.ReadAsStringAsync().Result;

                    foreach (string line in data.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                    {
                        GenreRanking genre = JsonSerializer.Deserialize<GenreRanking>(line);
                        genreRanks.Add(genre);
                    }
                    Console.WriteLine(genreRanks.Count);

                    AppsReqBinding reqBind = new AppsReqBinding(detail.store, detail.country, detail.date);
                    List<AggregatedRankModel> aggregatedRanks = await _aggregatedRankService.getAggregatedRanks(reqBind);

                    if (aggregatedRanks.Count == 0)
                    {
                        Console.WriteLine("Failed to get Aggregated Ranks");
                        return null;
                    }

                    List<Genre> genres = new List<Genre>();
                    for (int j = 0; j < genreRanks.Count; j++)
                    {
                        //Because of the Api AppMonsta trial subscribtion I choosed to get only 10 items
                        if (genres.Count == 10)
                        {
                            break;
                        }

                        GenreRanking gen = genreRanks[j];
                        for (int i = 0; i < aggregatedRanks.Count; i++)
                        {
                            AggregatedRankModel aggRank = aggregatedRanks[i];
                            if (gen.genre_id == aggRank.genre_id)
                            {
                                string first_app_logo = await _singleAppDetailsService.getAppIconAsync(aggRank.ranks[0].ToString(), detail.country, detail.store);
                                Genre g = new Genre(gen.name, aggRank.genre_id, first_app_logo);
                                genres.Add(g);

                                if (genres.Count == 10)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    return genres;
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
