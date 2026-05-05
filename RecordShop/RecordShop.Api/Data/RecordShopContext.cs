using Microsoft.EntityFrameworkCore;
using RecordShop.Api.Models.DataModels;
using System.Text.Json;

namespace RecordShop.Api.Data
{
    public class RecordShopContext : DbContext
    {
        private readonly string _albumFilePath = "Resources\\albums.json";

        public DbSet<Album> Albums { get; set; }
        public RecordShopContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var albumsJson = File.ReadAllText(_albumFilePath);
            var albums = JsonSerializer.Deserialize<List<Album>>(albumsJson);

            if (albums != null )
            {
                modelBuilder.Entity<Album>().HasData(albums);
            }
        }
    }
}
