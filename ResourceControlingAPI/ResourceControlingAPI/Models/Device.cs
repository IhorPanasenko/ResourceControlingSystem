using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ResourceControlingAPI.Models
{
    public class Device
    {
        [Key]
        public int DeviceId { get; set; }

        [ForeignKey("AddressId")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        public int MeterId { get; set; }

        [ForeignKey("MeterId")]
        [AllowNull]
        public Meter Meter { get; set; }
    }
}
