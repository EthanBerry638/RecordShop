namespace RecordShop.Api.Models.DTOs
{
    public record PutAlbumRequest(string Title, string Artist, decimal Price) {}
    public record PutAlbumResponse(int Id, string Title, string Artist, decimal Price) {}
}
