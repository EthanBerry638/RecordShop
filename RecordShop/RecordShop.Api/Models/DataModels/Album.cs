using System.ComponentModel.DataAnnotations;

namespace RecordShop.Api.Models.DataModels
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(150, ErrorMessage = "Title cannot exceed 150 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Artist is required")]
        [MaxLength(150, ErrorMessage = "Artist cannot exceed 150 characters")]
        public string Artist { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 2000000, ErrorMessage = "Price must be between 0.01 and 2,000,000")]
        public decimal Price { get; set; }
    }
}