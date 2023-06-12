namespace Apps_Review_Api.Models
{
    public class Genre
    {
        public string name {  get; set; }

        public string genre_id { get; set; }

        public string first_app_logo { get; set; }

        public Genre(string name, string genre_id, string first_app_logo)
        {
            this.name = name;
            this.genre_id = genre_id;
            this.first_app_logo = first_app_logo;
        }
    }
}
