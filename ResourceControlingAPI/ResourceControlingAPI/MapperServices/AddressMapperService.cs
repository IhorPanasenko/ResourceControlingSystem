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
            List<AddressDto> addressDtos = new List<AddressDto>();
            foreach (Address model in modelsList)
            {
                addressDtos.Add(AsDto(model));
            }
            return addressDtos;
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
