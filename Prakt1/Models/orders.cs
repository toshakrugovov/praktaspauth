namespace Prakt1.Models
{
    public class orders
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public DateTime order_date { get; set; }
        public string status { get; set; }
        public decimal total { get; set; }
    }
}
