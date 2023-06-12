namespace Apps_Review_Api.Models
{
    public class AppsReqBinding
    {
        //public string genre_id { get; set; }
        public string store { get; set; }
        public string country { get; set; }
        public string date { get; set; }

        public AppsReqBinding(string store,string country,string date) {
            //this.genre_id = id;
            this.store = store;
            this.country = country;
            this.date = date;
        }
    }
}
