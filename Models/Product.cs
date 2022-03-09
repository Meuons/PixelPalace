using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Product
    {
        public int? ProductID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }
        public int Amount { get; set; } = 0;
        public List<Order>? Orders { get; set; }
    }
}
