using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.Services
{
    public class AddressUpdateService : IUpdateService<Address, AddressDtoUpdate>
    {
        public void Update(Address model, AddressDtoUpdate dtoUpdate)
        {
            if(dtoUpdate.City != null)
            {
                model.City = dtoUpdate.City; 
            }
            if(dtoUpdate.StreetName != null)
            {
                model.StreetName = dtoUpdate.StreetName;
            }
            if(dtoUpdate.HouseNumber > 0)
            {
                model.HouseNumber = dtoUpdate.HouseNumber;
            }
            if(dtoUpdate.FlatNumber > 0)
            {
                model.FlatNumber = dtoUpdate.FlatNumber;
            }
        }
    }
}
