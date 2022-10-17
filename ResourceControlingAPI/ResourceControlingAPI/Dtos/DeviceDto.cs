using ResourceControlingAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceControlingAPI.Dtos
{
    public class DeviceDto
    {
        public int DeviceId { get; set; }
        public int AddressId { get; set; }
        public Address? Address { get; set; }
        public int MeterId { get; set; }
        public Meter? Meter { get; set; }
    }
}
