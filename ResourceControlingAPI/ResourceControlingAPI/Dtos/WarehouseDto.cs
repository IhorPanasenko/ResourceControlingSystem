using System.ComponentModel.DataAnnotations;

namespace ResourceControlingAPI.Dtos
{
    public class WarehouseDto
    {
        [Key]
        public int WarehouseId { get; set; }

        [Required]
        public int DevicePrice { get; set; }

        [Required]
        public int AvailableDevices { get; set; }
    }
}
