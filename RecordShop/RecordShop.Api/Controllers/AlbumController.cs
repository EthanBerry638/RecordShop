using Microsoft.AspNetCore.Mvc;
using RecordShop.Api.Models.DTOs;
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
        public async Task<IActionResult> GetAllAlbumsAsync()
        {
            var albums = await _albumService.GetAllAlbumsAsync();

            return Ok(albums);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbumByIdAsync(int id)
        {
            var album = await _albumService.GetAlbumByIdAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        [HttpPost("add")]
        public async Task<IActionResult> PostAlbumAsync(PostAlbumRequestResponse requestDTO)
        {
            var postedAlbum = await _albumService.PostAlbumAsync(requestDTO);

            return Created();
        }
    }
}
