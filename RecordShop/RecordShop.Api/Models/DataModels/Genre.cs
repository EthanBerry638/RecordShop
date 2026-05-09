namespace RecordShop.Api.Models.DataModels
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Album> Albums { get; set; } = new();
        public List<Track> Tracks { get; set; } = new();
        public List<Artist> Artists { get; set; } = new();
    }
}
