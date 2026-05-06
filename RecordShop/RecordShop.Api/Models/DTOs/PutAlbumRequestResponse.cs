using System.ComponentModel.DataAnnotations;

namespace RecordShop.Api.Models.DTOs
{
    public record PutAlbumRequest(
        [Required][MaxLength(150)] string Title,
        [Required][MaxLength(150)] string Artist,
        [Range(0, 2000000)] decimal Price
    );

    public record PutAlbumResponse(
        [Required] int Id,
        [Required][MaxLength(150)] string Title,
        [Required][MaxLength(150)] string Artist,
        [Range(0, 2000000)] decimal Price
    );
}
