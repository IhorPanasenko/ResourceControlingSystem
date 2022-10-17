using ResourceControlingAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceControlingAPI.Dtos
{
    public class RenterDto
    {
        [Key]
        public int RenterID { get; set; }

        [Required]
        public string? Login { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string? EmailAddress { get; set; }

        public bool IsSubscribed { get; set; } = false;

        [ForeignKey("AddressId")]
        public int AddressId { get; set; }
        public Address? Address { get; set; }
    }
}
