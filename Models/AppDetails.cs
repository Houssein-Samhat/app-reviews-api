namespace Apps_Review_Api.Models
{
    public class AppDetails
    {
        public string id { get; set; }
        public string app_name {  get; set; }
        public string publisher_name { get; set;}

        public List<string> genres { get; set; }

        public string icon_url { get; set; }

        public List<string> screenshot_urls { get; set; }

        public string description { get; set; }

        public double all_rating { get; set; }

        public AppDetails(string id, string app_name, string publisher_name, List<string> genres, string icon_url, List<string> screenshot_urls, string description, double all_rating)
        {
            this.id = id;
            this.app_name = app_name;
            this.publisher_name = publisher_name;
            this.genres = genres;
            this.icon_url = icon_url;
            this.screenshot_urls = screenshot_urls;
            this.description = description;
            this.all_rating = all_rating;
        }
    }
}
