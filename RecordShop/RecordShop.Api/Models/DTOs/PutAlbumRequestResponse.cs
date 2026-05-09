using System.ComponentModel.DataAnnotations;

namespace RecordShop.Api.Models.DTOs
{
    public record PutAlbumRequest(
        [Required][MaxLength(150, ErrorMessage = "Title cannot exceed 150 characters")] string Title,
        [Required][MaxLength(250, ErrorMessage = "Description cannot exceed 250 characters")] string Description,
        DateTime? ReleaseDate,
        [Range(0.01, 2000000, ErrorMessage = "Price must be between 0.01 and 2,000,000")] decimal Price
    );

    public record PutAlbumResponse(
        int Id,
        string Title,
        string Description,
        DateTime? ReleaseDate,
        decimal Price
    );
}
