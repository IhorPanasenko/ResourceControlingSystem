using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceControlingAPI.Data;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.MapperServices;
using ResourceControlingAPI.Models;
using ResourceControlingAPI.Services;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResourceControlingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MeterMapperService _mapperService;

        public MeterController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapperService = new MeterMapperService(mapper);
        }
        // GET: api/<MeterController>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var meters = await _dbContext.Meters.ToListAsync();
            var metersDto = _mapperService.AsDtoList(meters);
            return Ok(metersDto);
        }

        // GET api/<MeterController>/5
        [HttpGet]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var meter = await _dbContext.Meters.FindAsync(id);

            if(meter == null)
            {
                return NotFound($"Can't find Meter with id {id}");
            }

            var meterDto = _mapperService.AsDto(meter);
            return Ok(meterDto);
        }

        // POST api/<MeterController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General")]
        public async Task<IActionResult> Create(MeterDto meterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var meter = _mapperService.AsModel(meterDto);

            if(meter == null)
            {
                return BadRequest();
            }

            _dbContext.Meters.Add(meter);
            await _dbContext.SaveChangesAsync();
            meterDto = _mapperService.AsDto(meter);
            return Ok(meterDto);

        }

        // PUT api/<MeterController>/5
        [HttpPut]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General")]
        public async Task<IActionResult> Update([FromRoute]int id, MeterDtoUpdate meterDtoUpdate)
        {
            var meter = await _dbContext.Meters.FindAsync(id);

            if(meter == null)
            {
                return NotFound();
            }

            MeterUpdateService updateService = new MeterUpdateService();
            updateService.Update(meter, meterDtoUpdate);
            var meterDto = _mapperService.AsDto(meter);
            _dbContext.Meters.Update(meter);
            await _dbContext.SaveChangesAsync();
            return Ok(meterDto);
        }

        // DELETE api/<MeterController>/5
        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var meter = await _dbContext.Meters.FindAsync(id);

            if(meter == null)
            {
                return NotFound($"Can't find Meter with id {id}");
            }

            var meterDto = _mapperService.AsDto(meter);
            _dbContext.Meters.Remove(meter);
            await _dbContext.SaveChangesAsync();
            return Ok(meterDto);
        }
    }
}
