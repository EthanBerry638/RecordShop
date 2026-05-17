using Microsoft.AspNetCore.Mvc;
using RecordShop.Api.Services;

namespace RecordShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArtistsAsync()
        {
            var artists = await _artistService.GetAllArtistsAsync();

            return Ok(artists);
        }
    }
}
