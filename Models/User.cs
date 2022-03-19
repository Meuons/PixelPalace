using System.ComponentModel.DataAnnotations;
namespace Project.Models
{
    public class User
    {
        public int? UserID { get; set; }
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

    }
}
