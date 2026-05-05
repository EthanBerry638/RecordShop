using RecordShop.Api.Models.DataModels;

namespace RecordShop.Api.Repositories
{
    public interface IAlbumRepository
    {
        Task<List<Album>> GetAllAlbums();
    }
}
