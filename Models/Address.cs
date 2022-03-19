
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        
        [Required]
        public string? Country { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Street { get; set; }
        [Required]
        public string? Zipcode { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
