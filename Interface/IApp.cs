using Apps_Review_Api.Models;

namespace Apps_Review_Api.Interface
{
    public interface IApp
    {
        Task<List<AppDetails>> getAllApps(string genreId, string country, string date, string store);
        Task<AppDetails> getSingleApp(string appId, string store, string country);

        Task<string> getAppIconAsync(string appId, string country, string store);
    }
}
