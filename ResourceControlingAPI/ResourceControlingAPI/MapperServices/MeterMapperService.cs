using AutoMapper;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.MapperServices
{
    public class MeterMapperService : IMapperService<MeterDto, Meter>
    {
        private readonly IMapper _mapper;

        public MeterMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public MeterDto AsDto(Meter model)
        {
            return _mapper.Map<MeterDto>(model);
        }

        public List<MeterDto> AsDtoList(List<Meter> modelsList)
        {
            List<MeterDto> Dtos = new List<MeterDto>();
            foreach (Meter model in modelsList)
            {
                Dtos.Add(AsDto(model));
            }
            return Dtos;
        }

        public Meter AsModel(MeterDto dto)
        {
            return _mapper.Map<Meter>(dto);
        }

        public List<Meter> AsModelList(List<MeterDto> dtosList)
        {
            throw new NotImplementedException();
        }
    }
}
