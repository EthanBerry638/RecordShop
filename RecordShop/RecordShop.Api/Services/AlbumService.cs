using RecordShop.Api.Models.DataModels;
using RecordShop.Api.Models.DTOs;
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
            if (id <= 0) throw new ArgumentException();

            return await _albumRepository.GetAlbumByIdAsync(id);
        }

        public async Task<PostAlbumRequestResponse?> PostAlbumAsync(PostAlbumRequestResponse postAlbumDTO)
        {
            var placeholder = new PostAlbumRequestResponse("placeholder", "placeholder", 2);

            return placeholder;
        }
    }
}
