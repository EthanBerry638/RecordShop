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

        public async Task<PostAlbumResponse> PostAlbumAsync(PostAlbumRequest postAlbumDTO)
        {
            var title = postAlbumDTO.Title?.Trim();
            var artist = postAlbumDTO.Artist?.Trim();

            if (postAlbumDTO.Price < 0 || postAlbumDTO.Price >= 2000000) throw new InvalidPriceException();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(artist)) throw new EmptyStringException();

            if (title!.Length > 255 || artist!.Length > 255) throw new LongStringException();

            var postedAlbum = new Album
            {
                Title = title!,
                Artist = artist!,
                Price = postAlbumDTO.Price
            };

            var postResult = await _albumRepository.PostAlbumAsync(postedAlbum);

            return new PostAlbumResponse(postResult.Id, postResult.Title, postResult.Artist, postResult.Price);
        }

        public async Task<PutAlbumResponse?> PutAlbumAsync(PutAlbumRequest putAlbumDTO, int id)
        {
            var albumToUpdate = await GetAlbumByIdAsync(id);

            return null!;
        }
    }
}
