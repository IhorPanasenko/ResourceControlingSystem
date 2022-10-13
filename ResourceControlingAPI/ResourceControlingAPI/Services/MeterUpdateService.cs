using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.Services
{
    public class MeterUpdateService : IUpdateService<Meter, MeterDtoUpdate>
    {
        public void Update(Meter model, MeterDtoUpdate dtoUpdate)
        {
            if (dtoUpdate.Number != 0)
            {
                model.Number = dtoUpdate.Number;
            }
            if(dtoUpdate.Purpose!= null)
            {
                model.Purpose = dtoUpdate.Purpose;
            }
            if (dtoUpdate.MaximumAvailableValue > 0)
            {
                model.MaximumAvailableValue = dtoUpdate.MaximumAvailableValue;
            }
        }
    }
}
