using RecordShop.Api.Data;
using RecordShop.Api.Models.DataModels;

namespace RecordShop.Api.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly RecordShopContext _db;

        public ArtistRepository(RecordShopContext db)
        {
            _db = db;
        }

        public async Task<List<Artist>> GetAllArtistsAsync()
        {
            return null;
        }
    }
}
