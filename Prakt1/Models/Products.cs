namespace Prakt1.Models
{
    public class Products
    {
        public int id { get; set; } 
        public int category_id { get; set; } 
        public string name { get; set; } 
        public string description { get; set; }  
        public decimal price { get; set; }  
        public int stock { get; set; }  
        public string image_path { get; set; }  
    }
}
