using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecordShop.Api.Models.DataModels;
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

        [HttpGet]
        public async Task<IActionResult> GetAlbumByIdAsync(int id)
        {
            var album = await _albumService.GetAlbumByIdAsync(id);

            return Ok(album);
        }
    }
}
