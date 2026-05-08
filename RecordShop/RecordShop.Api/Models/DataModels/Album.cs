using System.ComponentModel.DataAnnotations;

namespace RecordShop.Api.Models.DataModels
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}