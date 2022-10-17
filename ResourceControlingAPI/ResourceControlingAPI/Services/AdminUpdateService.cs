using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.Services
{
    public class AdminUpdateService : IUpdateService<Admin, AdminDtoUpdate>
    {
        public void Update(Admin model, AdminDtoUpdate dtoUpdate)
        {
            if(dtoUpdate.EmailAddress != null)
            {
                model.EmailAddress = dtoUpdate.EmailAddress;
            }
            if(dtoUpdate.FirstName != null)
            {
                model.FirstName = dtoUpdate.FirstName;
            }
            if(dtoUpdate.SecondName != null)
            {
                model.SecondName = dtoUpdate.SecondName;
            }
            if(dtoUpdate.PhoneNumber != null)
            {
                model.PhoneNumber = dtoUpdate.PhoneNumber;
            }
            if(dtoUpdate.Login != null)
            {
                model.Login = dtoUpdate.Login;
            }
            if(dtoUpdate.Password != null)
            {
                model.Password = dtoUpdate.Password;    
            }
        }
    }
}
