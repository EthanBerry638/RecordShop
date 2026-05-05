using Microsoft.EntityFrameworkCore;
using RecordShop.Api.Data;
using RecordShop.Api.Models.DataModels;

namespace RecordShop.Api.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly RecordShopContext _db;

        public AlbumRepository(RecordShopContext db)
        {
            _db = db;
        }

        public async Task<List<Album>> GetAllAlbumsAsync()
        {
            return await _db.Albums.ToListAsync();
        }

        public async Task<Album> GetAlbumByIdAsync(int id)
        {
            return await _db.Albums.FirstAsync();
        }
    }
}
