using Apps_Review_Api.Models;

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using Apps_Review_Api.Interface;
using Apps_Review_Api.Services;

namespace Apps_Review_Api.Repositories
{
    public class SingleAppDetailsRepository : IApp
    {
        private readonly IConfiguration _config;
        private readonly GetAggregatedRankService _aggregatedRankService;

        public SingleAppDetailsRepository(IConfiguration config, GetAggregatedRankService aggregatedRankService)
        {
            _config = config;
            _aggregatedRankService = aggregatedRankService;
        }

        public async Task<List<AppDetails>> getAllApps(string genreId, string country, string date, string store)
        {
            AppsReqBinding appsReqBinding = new AppsReqBinding(store, country, date);

            List<AppDetails> allApps = new List<AppDetails>();

            List<AggregatedRankModel> aggregatedRanks = await _aggregatedRankService.getAggregatedRanks(appsReqBinding);
            

            if (aggregatedRanks == null)
            {
                Console.WriteLine("Error occured while : Aggregated Ranks is Empty");
                return allApps;
            }

            for (int i = 0; i < aggregatedRanks.Count; i++)
            {
                if (aggregatedRanks[i].genre_id == genreId)
                {
                    //This is the list of apps we need
                    var list_apps = aggregatedRanks[i].ranks;

                    for (int k = 0; k < list_apps.Count; k++)
                    {
                        //Because of the AppMonsta Api free trial we will only pick 10 Apps
                        if (allApps.Count == 10)
                        {
                            break;
                        }
                        AppDetails app_detail = await getSingleApp(list_apps[k], store, country);
                        allApps.Add(app_detail);
                    }
                    break;
                }
            }

            return allApps;
        }

        public async Task<AppDetails> getSingleApp(string appId, string store, string country)
        {
            using (var client = new HttpClient())
            {
                string baseUrl = $"https://api.appmonsta.com/v1/stores/{store}/details/{appId}.json";

                string username = _config.GetSection("AppSettings:Api_Key").Value;
                string password = _config.GetSection("AppSettings:Password_Key").Value;

                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

                string endpoint = $"api/endpoint?country={country}";
                HttpResponseMessage response = await client.GetAsync(baseUrl + endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    AppDetails app = JsonSerializer.Deserialize<AppDetails>(responseBody);

                    return app;
                }
                else
                {
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                }
                return null;
            }
        }


        public async Task<string> getAppIconAsync(string appId, string country, string store)
        {
            Console.WriteLine("The App ID is :" + appId);
            using (var client = new HttpClient())
            {
                string baseUrl = $"https://api.appmonsta.com/v1/stores/{store}/details/{appId}.json";

                string username = _config.GetSection("AppSettings:Api_Key").Value;
                string password = _config.GetSection("AppSettings:Password_Key").Value;

                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

                string endpoint = $"api/endpoint?country={country}";
                HttpResponseMessage response = await client.GetAsync(baseUrl + endpoint);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JsonDocument json = JsonDocument.Parse(responseBody);

                    string app_icon = json.RootElement.GetProperty("icon_url").GetString();

                    return app_icon;
                }
                else
                {
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                }
                return "Failed to get App Icon Url";
            }
        }
    }
}
