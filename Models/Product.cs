using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Project.Models
{
    public class Product
    {
        public int? ProductID { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Price { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }
        public int Amount { get; set; } = 0;
        public List<Order>? Orders { get; set; }
    }
}
