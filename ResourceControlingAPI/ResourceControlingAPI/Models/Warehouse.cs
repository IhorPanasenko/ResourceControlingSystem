using System.ComponentModel.DataAnnotations;

namespace ResourceControlingAPI.Models
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }

        [Required]  
        public int DevicePrice { get; set; }

        [Required]
        public int AvailableDevices { get; set; }

        List<Order> Orders { get; set; }
    }
}
