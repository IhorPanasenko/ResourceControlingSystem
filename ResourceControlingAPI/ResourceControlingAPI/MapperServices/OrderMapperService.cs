using AutoMapper;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.MapperServices
{
    public class OrderMapperService : IMapperService<OrderDto, Order>
    {
        private readonly IMapper _mapper;

        public OrderMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public OrderDto AsDto(Order model)
        {
            return _mapper.Map<OrderDto>(model);
        }

        public List<OrderDto> AsDtoList(List<Order> modelsList)
        {
            List<OrderDto> result = new List<OrderDto>();

            foreach (Order model in modelsList)
            {
                result.Add(AsDto(model));
            }

            return result;
        }

        public Order AsModel(OrderDto dto)
        {
            return _mapper.Map<Order>(dto);
        }

        public List<Order> AsModelList(List<OrderDto> dtosList)
        {
            throw new NotImplementedException();
        }
    }
}
