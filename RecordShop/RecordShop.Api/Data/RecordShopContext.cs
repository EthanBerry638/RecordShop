using Microsoft.EntityFrameworkCore;
using RecordShop.Api.Models.DataModels;

namespace RecordShop.Api.Data
{
    public class RecordShopContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public RecordShopContext(DbContextOptions options) : base(options) { }
    }
}
