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
            return await _albumRepository.GetAlbumByIdAsync(id);
        }

        public async Task<PostAlbumResponse> PostAlbumAsync(PostAlbumRequest postAlbumDTO)
        {
            var postedAlbum = new Album
            {
                Title = postAlbumDTO.Title,
                Artist = postAlbumDTO.Artist,
                Price = postAlbumDTO.Price
            };

            var postResult = await _albumRepository.PostAlbumAsync(postedAlbum);

            return new PostAlbumResponse(postResult.Id, postResult.Title, postResult.Artist, postResult.Price);
        }

        public async Task<PutAlbumResponse?> PutAlbumAsync(PutAlbumRequest putAlbumDTO, int id)
        {
            var albumToUpdate = await GetAlbumByIdAsync(id);

            if (albumToUpdate == null)
            {
                return null;
            }

            albumToUpdate.Title = putAlbumDTO.Title;
            albumToUpdate.Artist = putAlbumDTO.Artist;
            albumToUpdate.Price = putAlbumDTO.Price;

            var putResult = await _albumRepository.PutAlbumAsync(albumToUpdate);

            return new PutAlbumResponse(putResult.Id, putResult.Title, putResult.Artist, putResult.Price);
        }
    }
}
