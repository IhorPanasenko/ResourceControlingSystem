using System.ComponentModel.DataAnnotations;

namespace ResourceControlingAPI.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? StreetName { get; set; }

        [Required]
        public int HouseNumber { get; set; }

        [Required]
        public int FlatNumber { get; set; }

        public List<Device> Devices { get; set; }

    }
}
