using System.ComponentModel.DataAnnotations;

namespace ResourceControlingAPI.Dtos
{
    public class RenterDto
    {
        [Key]
        public int RenterID { get; set; }

        public string? Login { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        public string? EmailAddress { get; set; }

        public bool IsSubscribed { get; set; }
    }
}
