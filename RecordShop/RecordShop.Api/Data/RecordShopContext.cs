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
            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Title)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(a => a.Artist)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(a => a.Price)
                    .IsRequired()
                    .HasColumnType("decimal(9, 2)");

                entity.ToTable(t => t.HasCheckConstraint("CK_Price_MaxLimit", "[Price] <= 2000000.00"));
            });

            var albumsJson = File.ReadAllText(_albumFilePath);
            var albums = JsonSerializer.Deserialize<List<Album>>(albumsJson);

            if (albums != null)
            {
                modelBuilder.Entity<Album>().HasData(albums);
            }
        }
    }
}
