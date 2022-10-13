using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.Services
{
    public class OrderUpdateServcie : IUpdateService<Order, OrderDtoUpdate>
    {
        public void Update(Order model, OrderDtoUpdate dtoUpdate)
        {
            if(dtoUpdate.NumberOfDevices > 0)
            {
                model.NumberOfDevices = dtoUpdate.NumberOfDevices;
            }
            if (dtoUpdate.City != null)
            {
                model.City = dtoUpdate.City;
            }
            if(dtoUpdate.POstalOficeName!= null)
            {
                model.PostalOficeNumber = dtoUpdate.PostalOficeNumber;
            }
            if(dtoUpdate.PostalOficeNumber > 0)
            {
                model.PostalOficeNumber = dtoUpdate.PostalOficeNumber;
            }
            if (dtoUpdate.DateOfOrder != null)
            {
                model.DateOfOrder = dtoUpdate.DateOfOrder;
            }
            if(dtoUpdate.RenterId > 0)
            {
                model.RenterId = dtoUpdate.RenterId;
            }
            if (dtoUpdate.WarehouseId > 0)
            {
                model.WarehouseId = dtoUpdate.WarehouseId;
            }
        }
    }
}
