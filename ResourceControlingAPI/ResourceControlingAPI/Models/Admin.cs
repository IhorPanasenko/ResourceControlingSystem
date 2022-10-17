
using System.ComponentModel.DataAnnotations;

namespace ResourceControlingAPI.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        public string? Login { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string FirstName { get; set; } = "admin";

        [Required]
        public string SecondName { get; set; } = "admin";

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string? EmailAddress { get; set; }
    }
}
