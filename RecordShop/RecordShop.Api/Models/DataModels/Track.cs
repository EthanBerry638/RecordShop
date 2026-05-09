namespace RecordShop.Api.Models.DataModels
{
    public class Track
    {
        public int Id { get; set; }
        public int DurationInSec { get; set; }
        public int AblumId { get; set; }
        public Album Album { get; set; }
        public List<Genre> Genres { get; set; } = new();
    }
}
