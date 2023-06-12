using Org.BouncyCastle.Asn1.Cmp;

namespace Apps_Review_Api.Models
{
    public class GenreReqBinding
    {
        public string store { get; set; } = "android";

        public string country { get; set; } = "US";
        public string date { get; set; } = "2023-06-05";

        public GenreReqBinding(string store, string country, string date)
        {
            this.store = store;
            this.country = country;
            this.date = date;
        }
    }
}
