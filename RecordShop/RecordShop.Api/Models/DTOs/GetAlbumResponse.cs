namespace RecordShop.Api.Models.DTOs
{
    public record GetAlbumResponse
    (
        int Id,
        string Title,
        string Description,
        DateOnly? ReleaseDate,
        decimal Price
    )
    { }
}
