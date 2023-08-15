using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.Services
{
    public class RenterUpdateService : IUpdateService<Renter, RenterDtoUpdate>
    {
        public void Update(Renter model, RenterDtoUpdate dtoUpdate)
        {
            if (dtoUpdate.Login != null)
            {
                model.Login = dtoUpdate.Login;
            }
            if (dtoUpdate.Password != null)
            {
                 model.Password = dtoUpdate.Password;
            }
            if (dtoUpdate.EmailAddress != null)
            {
                model.EmailAddress = dtoUpdate.EmailAddress;
            }
            if (dtoUpdate.FirstName != null)
            {
                model.FirstName = dtoUpdate.FirstName;
            }
            if(dtoUpdate.SecondName != null)
            {
                model.SecondName = dtoUpdate.SecondName;
            }
            if (dtoUpdate.PhoneNumber != null)
            {
                model.PhoneNumber = dtoUpdate.PhoneNumber;
            }
            if (dtoUpdate.AddressId > 0)
            {
                model.AddressId = dtoUpdate.AddressId;
            }
            model.IsSubscribed = dtoUpdate.IsSubscribed;
            
        }
    }
}
