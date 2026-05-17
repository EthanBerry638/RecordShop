using RecordShop.Api.Models.DataModels;

namespace RecordShop.Api.Repositories
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetAllArtistsAsync();
    }
}
