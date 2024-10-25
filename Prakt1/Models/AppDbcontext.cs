using Microsoft.EntityFrameworkCore;


namespace Prakt1.Models
{
    public class AppDbcontext : DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<categories> categories { get; set; }

        public DbSet<order_items> order_items { get; set; }

        public DbSet<orders> orders { get; set; }

        public DbSet<roles> roles { get; set; }

        public DbSet<users> users { get; set; }

        public DbSet<product_reviews> product_reviews { get; set; }




        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {
            Database.EnsureCreated(); 
        }
    }
}
