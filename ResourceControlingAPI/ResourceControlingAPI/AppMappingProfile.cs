using AutoMapper;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI
{
    public class AppMappingProfile: Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Renter, RenterDto>();
            CreateMap<RenterDto, Renter>().ReverseMap();

            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, AddressDto>().ReverseMap();   
            
        }
    }
}
