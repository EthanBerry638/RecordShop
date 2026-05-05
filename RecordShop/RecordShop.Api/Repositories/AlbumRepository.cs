using RecordShop.Api.Models.DataModels;

namespace RecordShop.Api.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        public async Task<List<Album?>> GetAllAlbums()
        {
            return await Task.FromResult(new List<Album?>());
        }
    }
}
