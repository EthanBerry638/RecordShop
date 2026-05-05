using RecordShop.Api.Models.DataModels;
using RecordShop.Api.Repositories;

namespace RecordShop.Api.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly AlbumRepository _albumRepository;

        public AlbumService(AlbumRepository albumRepository)
        {
           _albumRepository = albumRepository;
        }

        public async Task<List<Album?>> GetAllAlbums()
        {
            return await _albumRepository.GetAllAlbums();
        }
    }
}
