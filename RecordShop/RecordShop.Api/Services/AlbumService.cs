using RecordShop.Api.Models.DataModels;
using RecordShop.Api.Models.DTOs;
using RecordShop.Api.Repositories;
using RecordShop.Api.CustomExceptions;

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
            if (postAlbumDTO.Price < 0 || postAlbumDTO.Price >= 2000000) throw new InvalidPriceException();

            if (string.IsNullOrWhiteSpace(postAlbumDTO.Title) || string.IsNullOrWhiteSpace(postAlbumDTO.Artist)) throw new EmptyStringException();

            var postedAlbum = new Album
            {
                Title = postAlbumDTO.Title,
                Artist = postAlbumDTO.Artist,
                Price = postAlbumDTO.Price
            };

            var postResult = await _albumRepository.PostAlbumAsync(postedAlbum);

            var responseDTO = new PostAlbumRequestResponse(postAlbumDTO.Title, postAlbumDTO.Artist, postResult.Price);

            return responseDTO;
        }
    }
}
