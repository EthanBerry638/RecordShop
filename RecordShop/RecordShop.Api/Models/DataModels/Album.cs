using System.ComponentModel.DataAnnotations;

namespace RecordShop.Api.Models.DataModels
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public List<Track> Tracks { get; set; } = new();
        public List<Artist> Artists { get; set; } = new();
        public List<Genre> Genres { get; set; } = new();
    }
}