using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceControlingAPI.Data;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.MapperServices;

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
        public async Task<IActionResult> GetAll()
        {
            var meters = await _dbContext.Meters.ToListAsync();
            var metersDto = _mapperService.AsDtoList(meters);
            return Ok(metersDto);
        }

        // GET api/<MeterController>/5
        [HttpGet("{id}")]
        public string Get([FromRoute] int id)
        {
            return "value";
        }

        // POST api/<MeterController>
        [HttpPost]
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

            return Ok(meter);

        }

        // PUT api/<MeterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MeterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
