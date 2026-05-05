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

        public async Task<List<Album>> GetAllAlbumsAsync()
        {
            return await _albumRepository.GetAllAlbumsAsync();
        }

        public async Task<Album?> GetAlbumByIdAsync(int id)
        {
            return await _albumRepository.GetAlbumByIdAsync(id);
        }
    }
}
