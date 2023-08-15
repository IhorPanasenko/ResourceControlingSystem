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
        public double HoursForWaiting { get; set; }
        public int MeterId { get; set; }

        public string ErrorText { get; set; } = string.Empty;

        [ForeignKey("MeterId")]
        [AllowNull]
        public Meter Meter { get; set; }
    }
}
