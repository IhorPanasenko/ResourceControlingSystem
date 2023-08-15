using AutoMapper;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.MapperServices
{
    public class WarehouseMapperService : IMapperService<WarehouseDto, Warehouse>
    {
        private readonly IMapper _mapper;

        public WarehouseMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public WarehouseDto AsDto(Warehouse model)
        {
            return _mapper.Map<WarehouseDto>(model);
        }

        public List<WarehouseDto> AsDtoList(List<Warehouse> modelsList)
        {
            List<WarehouseDto> result = new List<WarehouseDto>();
            foreach(Warehouse model in modelsList)
            {
                result.Add(AsDto(model));
            }
            return result;
        }

        public Warehouse AsModel(WarehouseDto dto)
        {
            return _mapper.Map<Warehouse>(dto);
        }

        public List<Warehouse> AsModelList(List<WarehouseDto> dtosList)
        {
            throw new NotImplementedException();
        }
    }
}
