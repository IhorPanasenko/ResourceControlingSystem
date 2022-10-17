using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceControlingAPI.Data;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.MapperServices;
using ResourceControlingAPI.Services;

namespace ResourceControlingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly AdminMapperService _mapperService;

        public AdminController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapperService = new AdminMapperService(mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var admins = await _dbContext.Admins.ToListAsync();
            var adminDtos = _mapperService.AsDtoList(admins);
            return Ok(adminDtos);
        }

        [HttpGet]
        [Route("{id=int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var admin = await _dbContext.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            var adminDto = _mapperService.AsDto(admin);
            return Ok(adminDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminDto adminDto)
        {
            if (adminDto == null)
            {
                return BadRequest();
            }

            var admin = _mapperService.AsModel(adminDto);
            await _dbContext.Admins.AddAsync(admin);
            await _dbContext.SaveChangesAsync();
            return Ok(adminDto);
        }

        [HttpDelete]
        [Route("{id=int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var admin = await _dbContext.Admins.FindAsync(id);
            
            if(admin == null)
            {
                return NotFound();
            }

            var adminDto = _mapperService.AsDto(admin);
            _dbContext.Admins.Remove(admin);
            await _dbContext.SaveChangesAsync();
            return Ok(adminDto);
        }

        [HttpPut]
        [Route("{id=int}")]
        public async Task<IActionResult> Update([FromRoute] int id, AdminDtoUpdate dtoUpdate)
        {
            var admin = await _dbContext.Admins.FindAsync(id);

            if(admin == null)
            {
                return NotFound();
            }

            AdminUpdateService updateService = new AdminUpdateService();
            updateService.Update(admin, dtoUpdate);
            var adminDto = _mapperService.AsDto(admin);
            _dbContext.Admins.Update(admin);
            await _dbContext.SaveChangesAsync();
            return Ok(adminDto);
        }

    }
}
