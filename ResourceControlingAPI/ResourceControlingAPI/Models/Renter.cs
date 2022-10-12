using System.ComponentModel.DataAnnotations;

namespace ResourceControlingAPI.Models
{
    public class Renter
    {
        [Key]
        [Required]
        public int RenterID { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public bool IsSubscribed { get; set; }
    }
}
