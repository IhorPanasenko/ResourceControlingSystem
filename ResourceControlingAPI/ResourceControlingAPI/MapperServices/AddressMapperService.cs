using AutoMapper;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.MapperServices
{
    public class AddressMapperService : IMapperService<AddressDto, Address>
    {
        private readonly IMapper _mapper;
        public AddressMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public AddressDto AsDto(Address model)
        {
           return _mapper.Map<AddressDto>(model);
        }

        public List<AddressDto> AsDtoList(List<Address> modelsList)
        {
            throw new NotImplementedException();
        }

        public Address AsModel(AddressDto dto)
        {
            return _mapper.Map<Address>(dto);
        }

        public List<Address> AsModelList(List<AddressDto> dtosList)
        {
            throw new NotImplementedException();
        }
    }
}
