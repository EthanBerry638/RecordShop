using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecordShop.Api.Services;

namespace RecordShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumSerivce)
        {
            _albumService = albumSerivce;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlbums()
        {
            return BadRequest();
        }
    }
}
