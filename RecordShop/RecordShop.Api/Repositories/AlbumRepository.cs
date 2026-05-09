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

        public async Task<Album?> GetAlbumByIdAsync(int id)
        {
            return await _db.Albums.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Album> PostAlbumAsync(Album postedAlbum)
        {
            await _db.Albums.AddAsync(postedAlbum);
            await _db.SaveChangesAsync();

            return postedAlbum;
        }

        public async Task<Album> PutAlbumAsync(Album albumReplacement)
        {
            await _db.SaveChangesAsync();

            return albumReplacement;
        }

        public async Task<bool> DeleteAlbumByIdAsync(int id)
        {
            var album = await _db.Albums.FindAsync(id);

            if (album == null) return false;

            _db.Albums.Remove(album);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
