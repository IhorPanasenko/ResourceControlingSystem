using ResourceControlingAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceControlingAPI.Dtos
{
    public class RenterDtoUpdate
    {
        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; }

        public bool IsSubscribed { get; set; }

        public int AddressId { get; set; }
    }
}
