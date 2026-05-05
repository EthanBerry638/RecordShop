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

        public async Task<List<Album?>> GetAllAlbums()
        {
            var allAlbums = await _albumRepository.GetAllAlbums();

            if (allAlbums == null)
            {
                return null!;
            }

            return allAlbums;
        }
    }
}
