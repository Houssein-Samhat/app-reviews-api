namespace Apps_Review_Api.Models
{
    public class AppGenre
    {
        public string id { get; set; }
        public string app_name { get; set; }
        public string publisher_name { get; set; }

        public string icon_url { get; set; }

        public double all_rating { get; set; }

        public AppGenre(string id,string app_name,string publisher_name,string icon,double rate) {
            this.id = id;
            this.app_name = app_name;
            this.publisher_name = publisher_name;
            this.icon_url = icon;
            this.all_rating = rate;
        }
    }
}
