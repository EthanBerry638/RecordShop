using Microsoft.EntityFrameworkCore;
using RecordShop.Api.Models.DataModels;

namespace RecordShop.Api.Data
{
    public class RecordShopContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public RecordShopContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().HasData(
                new Album { Id = 1, Title = "Thriller", Artist = "Michael Jackson", Price = 10.99M },
                new Album { Id = 2, Title = "Back In Black", Artist = "AC/DC", Price = 9.49M },
                new Album { Id = 3, Title = "The Dark Side of the Moon", Artist = "Pink Floyd", Price = 15.00M },
                new Album { Id = 4, Title = "Nevermind", Artist = "Nirvana", Price = 5.99M }
            );
        }
    }
}
