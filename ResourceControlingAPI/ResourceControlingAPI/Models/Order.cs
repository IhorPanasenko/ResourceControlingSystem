using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceControlingAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int NumberOfDevices { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? POstalOficeName { get; set; }

        [Required]
        public int PostalOficeNumber { get; set; }

        [Required]
        public DateTime? DateOfOrder { get; set; }

        [ForeignKey("RenterId")]
        public int RenterId { get; set; }
        public Renter? Renter { get; set; }

        [ForeignKey("WarehouseId")]
        public int WarehouseId { get; set; }    
        public Warehouse? Warehouse { get; set; }
    }
}
