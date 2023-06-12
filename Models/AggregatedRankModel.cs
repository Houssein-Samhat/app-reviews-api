namespace Apps_Review_Api.Models
{
    public class AggregatedRankModel
    {
        public List<string> ranks { get; set; }
        public string country { get; set; }
        public string rank_id { get; set; }

        public string genre_id { get; set; }

        public AggregatedRankModel(List<string> ranks, string country,string rank_id, string genre_id)
        {
            this.ranks = ranks;
            this.country = country;
            this.rank_id = rank_id;
            this.genre_id = genre_id;

        }
    }
}
