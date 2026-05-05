using RecordShop.Api.Models.DataModels;

namespace RecordShop.Api.Services
{
    public interface IAlbumService
    {
       Task<List<Album>> GetAllAlbumsAsync();
       Task<Album> GetAlbumByIdAsync(int id);
    }
}
