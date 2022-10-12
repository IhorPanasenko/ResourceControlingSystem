using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceControlingAPI.Data;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.Models;

namespace ResourceControlingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RenterController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public RenterController(IMapper mapper,ApplicationDbContext dbContext) 
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Renter> renters = await _dbContext.Renters.ToListAsync();
            var renterDto = _mapper.Map<RenterDto>(renters[0]);
            return Ok(renterDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RenterDto renterDto)
        {
            var renter = _mapper.Map<Renter>(renterDto);
            await _dbContext.AddAsync(renter);
            await _dbContext.SaveChangesAsync();
            return Ok(renter);
        }
    }
}
