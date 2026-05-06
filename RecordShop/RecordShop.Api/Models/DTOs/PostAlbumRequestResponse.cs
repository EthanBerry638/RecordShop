namespace RecordShop.Api.Models.DTOs
{
    public record PostAlbumRequest(string Title, string Artist, decimal Price) { }
    public record PostAlbumResponse(int Id, string Title, string Artist, decimal Price) {}
}
