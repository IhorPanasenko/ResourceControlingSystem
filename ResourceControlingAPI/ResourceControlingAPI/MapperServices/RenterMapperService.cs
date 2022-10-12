using AutoMapper;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.MapperServices
{
    public class RenterMapperService : IMapperService<RenterDto, Renter>
    {
        private readonly IMapper _mapper;

        public RenterMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public RenterDto AsDto(Renter model)
        {
            return _mapper.Map<RenterDto>(model);
        }

        public List<RenterDto> AsDtoList(List<Renter> modelsList)
        {
            List<RenterDto> renterDtos = new List<RenterDto>();
            foreach(Renter model in modelsList)
            {
                renterDtos.Add(AsDto(model));
            }
            return renterDtos;
        }

        public Renter AsModel(RenterDto dto)
        {
            throw new NotImplementedException();
        }

        public List<Renter> AsModelList(List<RenterDto> dtosList)
        {
            throw new NotImplementedException();
        }
    }
}
