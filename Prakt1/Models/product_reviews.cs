namespace Prakt1.Models
{
    public class product_reviews
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int user_id { get; set; }
        public int rating { get; set; }
        public string review { get; set; }
        public DateTime review_date { get; set; }
    }
}
