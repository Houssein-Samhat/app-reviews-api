namespace Apps_Review_Api.Models
{
    public class GenreRanking
    {
        public string genre_id { get; set; } = string.Empty;

        public string name { get; set; } = string.Empty;

        public GenreRanking(string genre_id, string name)
        {
            this.genre_id = genre_id;
            this.name = name;
        }
    }
}
