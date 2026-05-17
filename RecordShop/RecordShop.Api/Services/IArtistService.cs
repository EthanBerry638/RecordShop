using RecordShop.Api.Models.DTOs;

namespace RecordShop.Api.Services
{
    public interface IArtistService
    {
        Task<List<GetArtistResponse>> GetAllArtistsAsync();
    }
}
