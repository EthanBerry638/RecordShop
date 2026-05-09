using RecordShop.Api.Models.DataModels;
using RecordShop.Api.Models.DTOs;

namespace RecordShop.Api.Services
{
    public interface IAlbumService
    {
        Task<List<GetAlbumResponse>> GetAllAlbumsAsync();
        Task<GetAlbumResponse?> GetAlbumByIdAsync(int id);
        Task<PostAlbumResponse> PostAlbumAsync(PostAlbumRequest postAlbumDTO);
        Task<PutAlbumResponse?> PutAlbumAsync(PutAlbumRequest putAlbumDTO, int id);
        Task<bool> DeleteAlbumByIdAync(int id);
    }
}
