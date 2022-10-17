using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class AddressController : Controller
    {
        private readonly AddressMapperService _mapperService;
        private readonly ApplicationDbContext _dbContext;

        public AddressController(IMapper addressMapperService, ApplicationDbContext context)
        {
            _dbContext = context;
            _mapperService = new AddressMapperService(addressMapperService);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            List<Address> addresses = await _dbContext.Addresses.ToListAsync();
            List<AddressDto> addressDtos = _mapperService.AsDtoList(addresses);
            return Ok(addressDtos);
        }

        [HttpGet]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var address = await _dbContext.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound("Can't find address with such id");
            }

            var addressDto = _mapperService.AsDto(address);
            return Ok(addressDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddressDto addressDto)
        {
            var address = _mapperService.AsModel(addressDto);
            await _dbContext.AddAsync(address);
            await _dbContext.SaveChangesAsync();
            addressDto = _mapperService.AsDto(address);

            return Ok(addressDto);
        }

        [HttpPut]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General")]
        public async Task<IActionResult> Update(int id, AddressDtoUpdate addressDto)
        {
            var address = await _dbContext.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            AddressUpdateService addressUpdateService = new AddressUpdateService();
            addressUpdateService.Update(address, addressDto);
            _dbContext.Addresses.Update(address);
            await _dbContext.SaveChangesAsync();
            return Ok(address);
        }

        [HttpDelete]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var address = await _dbContext.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound($"Can't found address with this id = {id}");
            }

            var addressDto = _mapperService.AsDto(address);
            _dbContext.Addresses.Remove(address);
            await _dbContext.SaveChangesAsync();
            return Ok(addressDto);

        }
    }
}
