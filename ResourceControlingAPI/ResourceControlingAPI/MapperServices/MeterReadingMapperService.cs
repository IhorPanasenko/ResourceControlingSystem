using AutoMapper;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.MapperServices
{
    public class MeterReadingMapperService : IMapperService<MeterReadingDto, MeterReading>
    {
        private readonly IMapper _mapper;

        public MeterReadingMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public MeterReadingDto AsDto(MeterReading model)
        {
            return _mapper.Map<MeterReadingDto>(model);  
        }

        public List<MeterReadingDto> AsDtoList(List<MeterReading> modelsList)
        {
            List<MeterReadingDto> result = new List<MeterReadingDto>();

            foreach(MeterReading model in modelsList)
            {
                result.Add(AsDto(model));
            }

            return result; 
        }

        public MeterReading AsModel(MeterReadingDto dto)
        {
            return _mapper.Map<MeterReading>(dto);
        }

        public List<MeterReading> AsModelList(List<MeterReadingDto> dtosList)
        {
            throw new NotImplementedException();
        }
    }
}
