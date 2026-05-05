using RecordShop.Api.Models.DataModels;

namespace RecordShop.Api.Services
{
    public interface IAlbumService
    {
        public Task<Album?> GetAllAlbums();
    }
}
