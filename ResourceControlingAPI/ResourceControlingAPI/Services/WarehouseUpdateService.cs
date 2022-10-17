using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.Services
{
    public class WarehouseUpdateService : IUpdateService<Warehouse, WarehouseDtoUpdate>
    {
        public void Update(Warehouse model, WarehouseDtoUpdate dtoUpdate)
        {
            if (dtoUpdate.AvailableDevices > 0)
            {
                model.AvailableDevices = dtoUpdate.AvailableDevices;
            }
            if (dtoUpdate.DevicePrice > 0)
            {
                model.DevicePrice = dtoUpdate.DevicePrice;
            }
        }
    }
}
