namespace ResourceControlingAPI.Dtos
{
    public class OrderDtoUpdate
    {
        public int OrderId { get; set; }

        public int NumberOfDevices { get; set; }

        public string? City { get; set; }

        public string? POstalOficeName { get; set; }

        public int PostalOficeNumber { get; set; }

        public DateTime? DateOfOrder { get; set; }

        public int RenterId { get; set; }

        public int WarehouseId { get; set; }
    }
}
