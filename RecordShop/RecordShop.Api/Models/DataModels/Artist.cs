namespace RecordShop.Api.Models.DataModels
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public int Age { get; set; }
        public List<Genre> Genres { get; set; } = new();
        public List<Album> Albums { get; set; } = new();
    }
}
