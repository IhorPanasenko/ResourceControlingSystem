using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.Services
{
    public class DeviceUpdateService : IUpdateService<Device, DeviceDtoUpdate>
    {
        public void Update(Device model, DeviceDtoUpdate dtoUpdate)
        {
            if (dtoUpdate.AddressId > 0) 
            {
                model.AddressId = dtoUpdate.AddressId;
                model.HoursForWaiting = dtoUpdate.HoursForWaiting;
            }
        }
    }
}
