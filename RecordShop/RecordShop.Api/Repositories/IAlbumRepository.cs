using RecordShop.Api.Models.DataModels;

namespace RecordShop.Api.Repositories
{
    public interface IAlbumRepository
    {
        Task<List<Album>> GetAllAlbumsAsync();
        Task<Album?> GetAlbumByIdAsync(int id);
        Task<Album> PostAlbumAsync(Album postedAlbum);
        Task<Album> PutAlbumAsync(Album putAlbumDTO);
        Task<bool> DeleteAlbumByIdAync(int id);
    }
}
