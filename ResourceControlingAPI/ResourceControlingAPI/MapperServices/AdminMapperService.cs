using AutoMapper;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.MapperServices
{
    public class AdminMapperService : IMapperService<AdminDto, Admin>
    {
        private readonly IMapper _mapper;

        public AdminMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public AdminDto AsDto(Admin model)
        {
            return _mapper.Map<AdminDto>(model);    
        }

        public List<AdminDto> AsDtoList(List<Admin> modelsList)
        {
            List<AdminDto> result = new List<AdminDto>();

            foreach(Admin model in modelsList)
            {
                result.Add(AsDto(model));
            }

            return result;
        }

        public Admin AsModel(AdminDto dto)
        {
            return _mapper.Map<Admin>(dto);
        }

        public List<Admin> AsModelList(List<AdminDto> dtosList)
        {
            throw new NotImplementedException();
        }
    }
}
