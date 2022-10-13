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
            CreateMap<AddressDto, Address>().ReverseMap();

            CreateMap<Meter, MeterDto>();
            CreateMap<MeterDto, Meter>().ReverseMap();

            CreateMap<Warehouse, WarehouseDto>();
            CreateMap<WarehouseDto, Warehouse>().ReverseMap();
            
        }
    }
}
