using Microsoft.EntityFrameworkCore;
using RecordShop.Api.Data;
using RecordShop.Api.Models.DataModels;
using RecordShop.Api.Models.DTOs;

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

        public async Task<Album?> GetAlbumByIdAsync(int id)
        {
            return await _db.Albums.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<PostAlbumRequestResponse?> PostAlbumAsync(PostAlbumRequestResponse postedAlbum)
        {
            var album = new PostAlbumRequestResponse("test", "test", 40);

            return album;
        }
    }
}
