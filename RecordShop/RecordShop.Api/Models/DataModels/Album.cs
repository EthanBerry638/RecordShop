using System.ComponentModel.DataAnnotations;

namespace RecordShop.Api.Models.DataModels
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Artist { get; set; } = string.Empty;

        [Range(0, 2000000)]
        public decimal Price { get; set; }
    }
}