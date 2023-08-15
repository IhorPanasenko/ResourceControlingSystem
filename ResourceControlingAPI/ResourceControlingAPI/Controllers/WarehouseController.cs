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
    public class WarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly WarehouseMapperService _mapperService;

        public WarehouseController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapperService = new WarehouseMapperService(mapper);
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var warehouses = await _dbContext.Warehouses.ToListAsync();
            var dtos = _mapperService.AsDtoList(warehouses);
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id=int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var warehouse = await _dbContext.Warehouses.FindAsync(id);

            if (warehouse == null)
            {
                return NotFound();
            }

            var dto = _mapperService.AsDto(warehouse);
            return Ok(dto);
        }

        [HttpPost]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Create(WarehouseDto warehouseDto)
        {
            if (warehouseDto == null)
            {
                return BadRequest();
            }

            var warehouse = _mapperService.AsModel(warehouseDto);
            await _dbContext.Warehouses.AddAsync(warehouse);
            await _dbContext.SaveChangesAsync();
            warehouseDto = _mapperService.AsDto(warehouse);
            return Ok(warehouseDto);
        }

        [HttpPut]
        [Route("{id=int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, WarehouseDtoUpdate dtoUpdate)
        {
            var warehouse = await _dbContext.Warehouses.FindAsync(id);

            if (warehouse == null)
            {
                return NotFound();
            }

            WarehouseUpdateService updateService = new WarehouseUpdateService();
            updateService.Update(warehouse, dtoUpdate);
            var dto = _mapperService.AsDto(warehouse);
            _dbContext.Warehouses.Update(warehouse);
            await _dbContext.SaveChangesAsync();
            return Ok(dto);
        }

        [HttpDelete]
        [Route("{id=int}")]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var warehouse = await _dbContext.Warehouses.FindAsync(id);

            if (warehouse == null)
            {
                return NotFound();
            }

            _dbContext.Warehouses.Remove(warehouse);
             await _dbContext.SaveChangesAsync();
            var dto = _mapperService.AsDto(warehouse);
            return Ok(dto);
        }
    }
}
