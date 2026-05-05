using RecordShop.Api.Models.DataModels;
using RecordShop.Api.Repositories;

namespace RecordShop.Api.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
           _albumRepository = albumRepository;
        }

        public async Task<List<Album>> GetAllAlbums()
        {
            return await _albumRepository.GetAllAlbums();
        }
    }
}
