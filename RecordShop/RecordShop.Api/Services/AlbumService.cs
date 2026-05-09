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

        public async Task<List<GetAlbumResponse>> GetAllAlbumsAsync()
        {
            var albums = await _albumRepository.GetAllAlbumsAsync();

            return albums.Select(a => new GetAlbumResponse(
                a.Id,
                a.Title,
                a.Description,
                a.ReleaseDate,
                a.Price
            )).ToList();
        }

        public async Task<GetAlbumResponse?> GetAlbumByIdAsync(int id)
        {
            var album = await _albumRepository.GetAlbumByIdAsync(id);

            if (album == null) return null;

            return new GetAlbumResponse
            (
                album!.Id,
                album.Title,
                album.Description,
                album.ReleaseDate,
                album.Price
            );
        }

        public async Task<PostAlbumResponse> PostAlbumAsync(PostAlbumRequest postAlbumDTO)
        {
            var postedAlbum = new Album
            {
                Title = postAlbumDTO.Title,
                Description = postAlbumDTO.Description,
                ReleaseDate = postAlbumDTO.ReleaseDate,
                Price = postAlbumDTO.Price
            };

            var postResult = await _albumRepository.PostAlbumAsync(postedAlbum);

            return new PostAlbumResponse(postResult.Id, postResult.Title, postResult.Description, postResult.ReleaseDate, postResult.Price);
        }

        public async Task<PutAlbumResponse?> PutAlbumAsync(PutAlbumRequest putAlbumDTO, int id)
        {
            var albumToUpdate = await _albumRepository.GetAlbumByIdAsync(id);

            if (albumToUpdate == null)
            {
                return null;
            }

            albumToUpdate.Title = putAlbumDTO.Title;
            albumToUpdate.Description = putAlbumDTO.Description;
            albumToUpdate.ReleaseDate = putAlbumDTO.ReleaseDate;
            albumToUpdate.Price = putAlbumDTO.Price;

            var putResult = await _albumRepository.PutAlbumAsync(albumToUpdate);

            return new PutAlbumResponse(putResult.Id, putResult.Title, putResult.Description, putResult.ReleaseDate, putResult.Price);
        }

        public async Task<bool> DeleteAlbumByIdAsync(int id)
        {
            var albumToDelete = await _albumRepository.GetAlbumByIdAsync(id);

            if (albumToDelete == null) return false;

            return await _albumRepository.DeleteAlbumByIdAsync(id);
        }
    }
}
