using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceControlingAPI.Data;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.MapperServices;
using ResourceControlingAPI.Services;
using System.Data;

namespace ResourceControlingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DeviceController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DeviceMapperService _mapperService;

        public DeviceController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapperService = new DeviceMapperService(mapper);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var devices = await _dbContext.Devices.Include(d => d.Address).Include(d => d.Meter).ToListAsync();
            var dtos = _mapperService.AsDtoList(devices);
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public IActionResult Get([FromRoute] int id)
        {
            var device = _dbContext.Devices.Where(d => d.DeviceId == id).Include(d => d.Address).Include(d => d.Meter).ToList().FirstOrDefault();

            if (device == null)
            {
                return NotFound();
            }

            var dto = _mapperService.AsDto(device);
            return Ok(dto);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Create(DeviceDto deviceDto)
        {
            var device = _mapperService.AsModel(deviceDto);
            var address = await _dbContext.Addresses.FindAsync(deviceDto.AddressId);
            var meter = await _dbContext.Meters.FindAsync(deviceDto.MeterId);

            if (address == null)
            {
                return NotFound("");
            }
            if (meter == null)
            {
                return NotFound();
            }

            device.Address = address;
            device.Meter = meter;
            await _dbContext.Devices.AddAsync(device);
            await _dbContext.SaveChangesAsync();
            deviceDto = _mapperService.AsDto(device);
            return Ok(deviceDto);
        }

        [HttpDelete]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var device = await _dbContext.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            var dto = _mapperService.AsDto(device);
            _dbContext.Devices.Remove(device);
            await _dbContext.SaveChangesAsync();
            return Ok(dto);
        }

        [HttpPut]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, DeviceDtoUpdate dtoUpdate)
        {
            var device = await _dbContext.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            DeviceUpdateService updateService = new DeviceUpdateService();
            updateService.Update(device, dtoUpdate);
            var address = await _dbContext.Addresses.FindAsync(device.AddressId);
            var meter = await _dbContext.Meters.FindAsync(device.MeterId);

            if (address == null)
            {
                return NotFound();
            }
            if (meter == null)
            {
                return NotFound();
            }

            device.Address = address;
            device.Meter = meter;
            var dto = _mapperService.AsDto(device);
            _dbContext.Devices.Update(device);
            await _dbContext.SaveChangesAsync();
            return Ok(dto);
        }

    }
}
