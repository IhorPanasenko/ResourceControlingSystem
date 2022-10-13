using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.Dtos
{
    public class DeviceDtoUpdate
    {
        public int DeviceId { get; set; }
        public int AddressId { get; set; }
        public int MeterId { get; set; }
    }
}
