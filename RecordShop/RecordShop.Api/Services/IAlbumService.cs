using RecordShop.Api.Models.DataModels;
using RecordShop.Api.Models.DTOs;

namespace RecordShop.Api.Services
{
    public interface IAlbumService
    {
        Task<List<Album>> GetAllAlbumsAsync();
        Task<Album?> GetAlbumByIdAsync(int id);
        Task<PostAlbumResponse> PostAlbumAsync(PostAlbumRequest postAlbumDTO);
    }
}
