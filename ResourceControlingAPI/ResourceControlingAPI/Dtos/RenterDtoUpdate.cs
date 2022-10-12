namespace ResourceControlingAPI.Dtos
{
    public class RenterDtoUpdate
    {
        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? EmailAddress { get; set; }

        public bool IsSubscribed { get; set; }
    }
}
