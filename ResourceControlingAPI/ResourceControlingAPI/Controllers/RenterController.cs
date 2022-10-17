using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceControlingAPI.Data;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.MapperServices;
using ResourceControlingAPI.Models;
using ResourceControlingAPI.Services;

namespace ResourceControlingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RenterController : Controller
    {
        private readonly RenterMapperService _mapperService;
        private readonly ApplicationDbContext _dbContext;

        public RenterController(IMapper mapper, ApplicationDbContext dbContext)
        {
            _mapperService = new RenterMapperService(mapper);
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var renters = await _dbContext.Renters.Include(r => r.Address).ToListAsync();
            var renterDtos = _mapperService.AsDtoList(renters);
            return Ok(renterDtos);
        }

        [HttpGet]
        [Route("{id=int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var renter = _dbContext.Renters.Where(r=> r.RenterID == id).Include(r=>r.Address).ToList().FirstOrDefault();

            if(renter== null)
            {
                return NotFound();
            }
                
            var renterDto = _mapperService.AsDto(renter);
            return Ok(renterDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RenterDto renterDto)
        {
            var renter = _mapperService.AsModel(renterDto);

            if (renter.Address == null)
            {
                var address = await _dbContext.Addresses.FindAsync(renter.AddressId);

                if (address == null)
                {
                    return NotFound();
                }

                renter.Address = address;
            }

            await _dbContext.AddAsync(renter);
            await _dbContext.SaveChangesAsync();
            renterDto = _mapperService.AsDto(renter);
            return Ok(renterDto);
        }

        [HttpDelete]
        [Route("{id=int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var renter = await _dbContext.Renters.FindAsync(id);

            if (renter == null)
            {
                return NotFound("Invalid Renter Id");
            }

            var renterDto = _mapperService.AsDto(renter);
            _dbContext.Renters.Remove(renter);
            await _dbContext.SaveChangesAsync();
            return Ok(renterDto);
        }

        [HttpPut]
        [Route("{id=int}")]
        public async Task<IActionResult> Update([FromRoute] int id, RenterDtoUpdate renterDto)
        {
            var renter = await _dbContext.Renters.FindAsync(id);

            if(renter == null)
            {
                return NotFound("Invalid Renter Id");
            }
            
            RenterUpdateService renterUpdateService = new RenterUpdateService();
            renterUpdateService.Update(renter, renterDto);
            var address = await _dbContext.Addresses.FindAsync(renter.AddressId);

            if(address == null)
            {
                return NotFound();
            }

            renter.Address = address;
            _dbContext.Renters.Update(renter);
            await _dbContext.SaveChangesAsync();
            return Ok(renter);
        }

    }
}
