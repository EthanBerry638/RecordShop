using Microsoft.EntityFrameworkCore;

namespace RecordShop.Api.Data
{
    public class RecordShopContext : DbContext
    {
        public RecordShopContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
