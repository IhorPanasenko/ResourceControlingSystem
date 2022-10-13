using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ResourceControlingAPI.Data;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.MapperServices;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MeterReadingMapperService _meterReadingmapperService;
        private readonly MeterMapperService _meterMapperService;

        public MeterReadingController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _meterReadingmapperService = new MeterReadingMapperService(mapper);
            _meterMapperService = new MeterMapperService(mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var meterReadings = await _dbContext.MeterReadings.Include(mR => mR.Meter).ToListAsync();
            var dtos = _meterReadingmapperService.AsDtoList(meterReadings);
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id=int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var meterReading = _dbContext.MeterReadings.Where(m => m.MeterReadingId == id).Include(m => m.Meter).ToList().FirstOrDefault();

            if(meterReading == null)
            {
                return NotFound();
            }

            var dto = _meterReadingmapperService.AsDto(meterReading);
            return Ok(dto);
        }
    }
}
