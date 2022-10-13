using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ResourceControlingAPI.Data;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.MapperServices;
using ResourceControlingAPI.Models;
using ResourceControlingAPI.Services;

namespace ResourceControlingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MeterReadingMapperService _meterReadingMapperService;
        private readonly MeterMapperService _meterMapperService;

        public MeterReadingController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _meterReadingMapperService = new MeterReadingMapperService(mapper);
            _meterMapperService = new MeterMapperService(mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var meterReadings = await _dbContext.MeterReadings.Include(mR => mR.Meter).ToListAsync();
            var dtos = _meterReadingMapperService.AsDtoList(meterReadings);
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

            var dto = _meterReadingMapperService.AsDto(meterReading);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MeterReadingDto meterReadingDto)
        {
            var meterReading = _meterReadingMapperService.AsModel(meterReadingDto);
            var meter = await _dbContext.Meters.FindAsync(meterReadingDto.MeterId);

            if(meter == null)
            {
                return NotFound($"can't find meter with such Id {meterReadingDto.MeterId}");
            }

            await _dbContext.MeterReadings.AddAsync(meterReading);
            await _dbContext.SaveChangesAsync();
            meterReadingDto = _meterReadingMapperService.AsDto(meterReading);
            return Ok(meterReadingDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var meterReading = await _dbContext.MeterReadings.FindAsync(id);

            if(meterReading == null)
            {
                return NotFound("");
            }
            var dto = _meterReadingMapperService.AsDto(meterReading);
            meterReading.Meter = null;
            _dbContext.MeterReadings.Remove(meterReading);
            await _dbContext.SaveChangesAsync();
            return Ok(dto);
        }

        [HttpPut]
        [Route("{id=int}")]
        public async Task<IActionResult> Update([FromRoute] int id, MeterReadingDtoUpdate dtoUpdate)
        {
            var meterReading = _dbContext.MeterReadings.Where(m => m.MeterReadingId == id).Include(m => m.Meter).ToList().FirstOrDefault();

            if(meterReading == null)
            {
                return NotFound();
            }

            MeterReadingUpdateService meterReadingService = new MeterReadingUpdateService();
            meterReadingService.Update(meterReading, dtoUpdate);
            var meter = await _dbContext.Meters.FindAsync(dtoUpdate.MeterId);

            if(meter == null)
            {
                return NotFound();
            }

            meterReading.Meter = meter;
            var meterReadingDto = _meterReadingMapperService.AsDto(meterReading);
            _dbContext.MeterReadings.Update(meterReading);
            await _dbContext.SaveChangesAsync();
            return Ok(meterReadingDto);
        }
    }
}
