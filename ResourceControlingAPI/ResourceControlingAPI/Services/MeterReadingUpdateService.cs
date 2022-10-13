using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.Services
{
    public class MeterReadingUpdateService : IUpdateService<MeterReading, MeterReadingDtoUpdate>
    {
        public void Update(MeterReading model, MeterReadingDtoUpdate dtoUpdate)
        {
            if (dtoUpdate.DateTimeReading != null)
            {
                model.DateTimeReading = dtoUpdate.DateTimeReading;
            }
            if(dtoUpdate.ReadingNumbers > 0)
            {
                model.ReadingNumbers = dtoUpdate.ReadingNumbers;
            }
            if(dtoUpdate.MeterId > 0)
            {
                model.MeterId = dtoUpdate.MeterId;
            }
        }
    }
}
