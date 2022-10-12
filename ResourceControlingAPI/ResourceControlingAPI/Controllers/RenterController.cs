﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceControlingAPI.Data;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.MapperServices;
using ResourceControlingAPI.Models;

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
            List<Renter> renters = await _dbContext.Renters.ToListAsync();
            List<RenterDto> renterDtos = _mapperService.AsDtoList(renters);
            return Ok(renterDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var renter = await _dbContext.Renters.FindAsync(id);

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
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid parameters");
            }

            Renter renter = _mapperService.AsModel(renterDto);
            await _dbContext.AddAsync(renter);
            await _dbContext.SaveChangesAsync();
            return Ok(renter);
        }
    }
}
