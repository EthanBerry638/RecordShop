using System.ComponentModel.DataAnnotations;

namespace RecordShop.Api.Models.DataModels
{
    public class Album
    {
        public int Id { get; init; }
        [MaxLength(255)]
        public required string Title { get; set; }
        [MaxLength(255)]
        public required string Artist { get; set; }
        public decimal Price { get; set; }
    }
}
