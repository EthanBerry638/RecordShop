namespace RecordShop.Api.Models.DataModels
{
    public class Album
    {
        public int Id { get; init; }
        public required string Title { get; set; }
        public required string Artist { get; set; }
        public decimal Price { get; set; }
    }
}
