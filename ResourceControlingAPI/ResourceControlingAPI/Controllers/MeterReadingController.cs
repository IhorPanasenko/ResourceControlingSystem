using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ResourceControlingAPI.Data;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.MapperServices;
using ResourceControlingAPI.Models;
using ResourceControlingAPI.Services;
using System.Data;

namespace ResourceControlingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MeterReadingMapperService _mapperService;

        public MeterReadingController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapperService = new MeterReadingMapperService(mapper);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var meterReadings = await _dbContext.MeterReadings.Include(mR => mR.Meter).ToListAsync();
            var dtos = _mapperService.AsDtoList(meterReadings);
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var meterReading = _dbContext.MeterReadings.Where(m => m.MeterReadingId == id).Include(m => m.Meter).ToList().FirstOrDefault();

            if(meterReading == null)
            {
                return NotFound();
            }

            var dto = _mapperService.AsDto(meterReading);
            return Ok(dto);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General")]
        public async Task<IActionResult> Create(MeterReadingDto meterReadingDto)
        {
            var meterReading = _mapperService.AsModel(meterReadingDto);
            var meter = await _dbContext.Meters.FindAsync(meterReadingDto.MeterId);

            if(meter == null)
            {
                return NotFound($"can't find meter with such Id {meterReadingDto.MeterId}");
            }

            meterReading.Meter = meter;
            await _dbContext.MeterReadings.AddAsync(meterReading);
            await _dbContext.SaveChangesAsync();
            meterReadingDto = _mapperService.AsDto(meterReading);
            return Ok(meterReadingDto);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var meterReading = await _dbContext.MeterReadings.FindAsync(id);

            if(meterReading == null)
            {
                return NotFound("");
            }
            var dto = _mapperService.AsDto(meterReading);
            meterReading.Meter = null;
            _dbContext.MeterReadings.Remove(meterReading);
            await _dbContext.SaveChangesAsync();
            return Ok(dto);
        }

        [HttpPut]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General")]
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
            var meterReadingDto = _mapperService.AsDto(meterReading);
            _dbContext.MeterReadings.Update(meterReading);
            await _dbContext.SaveChangesAsync();
            return Ok(meterReadingDto);
        }
    }
}
