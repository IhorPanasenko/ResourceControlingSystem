using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceControlingAPI.Models
{
    public class Device
    {
        [Key]
        public int DeviceId { get; set; }

        //[ForeignKey("AddressId")]
        //public int AddressId { get; set; }
        //public Address Address { get; set; }

        [ForeignKey("MeterId")]
        public int MeterId { get; set; }
        public Meter Meter { get; set; }
    }
}
