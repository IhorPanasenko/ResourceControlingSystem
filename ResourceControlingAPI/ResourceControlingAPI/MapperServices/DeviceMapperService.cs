using AutoMapper;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.MapperServices
{
    public class DeviceMapperService :IMapperService<DeviceDto, Device>
    {
        private readonly IMapper _mapper;

        public DeviceMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public DeviceDto AsDto(Device model)
        {
            return _mapper.Map<DeviceDto>(model);
        }

        public List<DeviceDto> AsDtoList(List<Device> modelsList)
        {
            List < DeviceDto > result = new List<DeviceDto>();

            foreach(Device model in modelsList)
            {
                result.Add(AsDto(model));
            }

            return result;
        }

        public Device AsModel(DeviceDto dto)
        {
            return _mapper.Map<Device>(dto);
        }

        public List<Device> AsModelList(List<DeviceDto> dtosList)
        {
            throw new NotImplementedException();
        }
    }
}
