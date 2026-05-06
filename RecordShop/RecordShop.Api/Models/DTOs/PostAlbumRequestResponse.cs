using System.ComponentModel.DataAnnotations;

namespace RecordShop.Api.Models.DTOs
{
    public record PostAlbumRequest(
        [Required][MaxLength(150, ErrorMessage = "Title cannot exceed 150 characters")] string Title,
        [Required][MaxLength(150, ErrorMessage = "Artist cannot exceed 150 characters")] string Artist,
        [Range(0.01, 2000000, ErrorMessage = "Price must be between 0.01 and 2,000,000")] decimal Price
    );

    public record PostAlbumResponse(
        [Required] int Id,
        [Required][MaxLength(150, ErrorMessage = "Title cannot exceed 150 characters")] string Title,
        [Required][MaxLength(150, ErrorMessage = "Artist cannot exceed 150 characters")] string Artist,
        [Range(0.01, 2000000, ErrorMessage = "Price must be between 0.01 and 2,000,000")] decimal Price
    );
}
