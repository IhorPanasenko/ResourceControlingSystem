using System.ComponentModel.DataAnnotations;

namespace ResourceControlingAPI.Dtos
{
    public class AdminDtoUpdate
    {
        public string? Login { get; set; }

        public string? Password { get; set; }

        public string FirstName { get; set; } = "admin";

        public string SecondName { get; set; } = "admin";

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; }
    }
}
