using RecordShop.Api.Models.DTOs;
using RecordShop.Api.Repositories;

namespace RecordShop.Api.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<List<GetArtistResponse>> GetAllArtistsAsync()
        {
            var artists = await _artistRepository.GetAllArtistsAsync();

            return artists.Select(a => new GetArtistResponse
            (   a.Id,
                a.Name,
                a.Bio,
                a.Age
            )).ToList();
        }
    }
}
