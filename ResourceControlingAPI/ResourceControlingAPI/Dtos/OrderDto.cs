using ResourceControlingAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceControlingAPI.Dtos
{
    public class OrderDto
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

        public int RenterId { get; set; }

        public RenterDto? Renter { get; set; }

        public int WarehouseId { get; set; }
        public WarehouseDto? Warehouse { get; set; }
    }
}
