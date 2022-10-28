using AutoMapper;
using AutoMapper.Configuration.Conventions;
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
        
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var meters = await _dbContext.Meters.ToListAsync();
            var metersDto = _mapperService.AsDtoList(meters);
            return Ok(metersDto);
        }

        
        [HttpGet]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var meter = await _dbContext.Meters.FindAsync(id);

            if (meter == null)
            {
                return NotFound($"Can't find Meter with id {id}");
            }

            var meterDto = _mapperService.AsDto(meter);
            return Ok(meterDto);
        }

        
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Create(MeterDto meterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var meter = _mapperService.AsModel(meterDto);

            if (meter == null)
            {
                return BadRequest();
            }

            _dbContext.Meters.Add(meter);
            await _dbContext.SaveChangesAsync();
            meterDto = _mapperService.AsDto(meter);
            return Ok(meterDto);

        }

        
        [HttpPut]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, MeterDtoUpdate meterDtoUpdate)
        {
            var meter = await _dbContext.Meters.FindAsync(id);

            if (meter == null)
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

        
        [HttpDelete]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var meter = await _dbContext.Meters.FindAsync(id);

            if (meter == null)
            {
                return NotFound($"Can't find Meter with id {id}");
            }

            var meterDto = _mapperService.AsDto(meter);
            _dbContext.Meters.Remove(meter);
            await _dbContext.SaveChangesAsync();
            return Ok(meterDto);
        }

        [HttpGet]
        [Route("UsingForToday/{meterId=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> ResourcesForToday([FromRoute]int meterId)
        {
            var meter = await _dbContext.Meters.FindAsync(meterId);

            if (meter == null)
            {
                return NotFound("No such meter");
            }

            var yesterdayMeterReadings = await _dbContext.MeterReadings
                .Where(mr => mr.MeterId == meterId && mr.DateTimeReading.Value.Day == DateTime.Today.Day - 1)
                .ToListAsync();

            if(yesterdayMeterReadings.Count == 0)
            {
                return BadRequest("Can't find reading for yesterday");
            }

            var todayMeterReadings = await _dbContext.MeterReadings
                .Where(mr => mr.MeterId == meterId && mr.DateTimeReading.Value.Day == DateTime.Today.Day)
                .ToListAsync();

            if(todayMeterReadings.Count == 0)
            {
                return BadRequest("Can't find todays meter readings");
            }

            var lastYesterdayReading = yesterdayMeterReadings
                .Where(mr => mr.DateTimeReading == (yesterdayMeterReadings.Max(mr => mr.DateTimeReading)))
                .ToList()[0].ReadingNumbers;

            var lastTodaymeterReading = todayMeterReadings
                .Where(mr => mr.DateTimeReading == (todayMeterReadings.Max(mr => mr.DateTimeReading)))
                .ToList()[0].ReadingNumbers;

            var minus = lastTodaymeterReading - lastYesterdayReading;
            return Ok(minus);
        }

        [HttpGet]
        [Route("UsingPerWeek/{weekNumber=int}/{meterId=int}/{monthNumber=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> ReadingsForWeek([FromRoute] int meterId, [FromRoute] int weekNumber, [FromRoute] int monthNumber)
        {
            var meter = await _dbContext.Meters.FindAsync(meterId);

            if (meter == null)
            {
                return NotFound("can't find this meter");
            }

            var readingsForWeek = _dbContext.MeterReadings
                .Where(mr => mr.DateTimeReading.Value.Day <= weekNumber * 7 && mr.DateTimeReading.Value.Day >= (weekNumber - 1) * 7 + 1 && mr.DateTimeReading.Value.Month == monthNumber && mr.MeterId == meter.MeterId)
                .ToList();

            List<object> minusList = new List<object>();

            for(int i=0; i< readingsForWeek.Count; ++i)
            {
                if (i < readingsForWeek.Count - 1)
                {
                    minusList.Add(new
                    {
                        dayUsing = readingsForWeek[i + 1].ReadingNumbers - readingsForWeek[i].ReadingNumbers,
                        startPeriod = readingsForWeek[i].DateTimeReading!.Value.Day,
                        endPeriod = readingsForWeek[i+1].DateTimeReading!.Value.Day,
                    });
                }
            }

            return Ok(minusList);
        }

        [HttpGet]
        [Route("UsingPerMonth/{yearNumber=int}/{meterId=int}/{monthNumber=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]

        public async Task<IActionResult> ReadingsForMonth([FromRoute] int meterId, [FromRoute] int yearNumber, [FromRoute] int monthNumber)
        {
            var meter = await _dbContext.Meters.FindAsync(meterId);

            if (meter == null)
            {
                return NotFound("can't find this meter");
            }

            var readingsForMonth = _dbContext.MeterReadings
                .Where(mr => mr.DateTimeReading!.Value.Month == monthNumber && mr.DateTimeReading.Value.Year == yearNumber && mr.MeterId == meter.MeterId)
                .ToList();

            List<object> minusList = new List<object>();

            for(int i=0; i<readingsForMonth.Count; ++i)
            {
                if (i < readingsForMonth.Count - 1)
                {
                    minusList.Add(new
                    {
                        dayUsing = readingsForMonth[i + 1].ReadingNumbers - readingsForMonth[i].ReadingNumbers,
                        startPeriod = readingsForMonth[i].DateTimeReading!.Value.Day,
                        endPeriod = readingsForMonth[i + 1].DateTimeReading!.Value.Day
                    }) ;
                }
            }
            return Ok(minusList);
        }

        [HttpGet]
        [Route("AmountResourcesLeftPercents/{meterId=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> AmountResourcesLeftPercents([FromRoute]int meterId)
        {
            var meter = await _dbContext.Meters.FindAsync(meterId);
            if (meter == null)
            {
                return NotFound("No such Meter found");
            }

            var readingsForCurrentMonth = _dbContext.MeterReadings
                .Where(mr => mr.DateTimeReading.Value.Month == DateTime.Today.Month && mr.DateTimeReading.Value.Year == DateTime.Today.Year && mr.MeterId == meter.MeterId)
                .Select(mr => mr.ReadingNumbers)
                .ToList();

            var readingFirstDay = readingsForCurrentMonth
                .Where(r => r == readingsForCurrentMonth.Min())
                .ToList()[0];

            var lastReading = readingsForCurrentMonth
                .Where(r => r == readingsForCurrentMonth.Max())
                .ToList()[0];

            var razn = lastReading - readingFirstDay;
            var left = Math.Round((1- razn / meter.MaximumAvailableValue) * 100);
            return Ok(left);
        }

        [HttpGet]
        [Route("AmountResourcesUsed/{meterId=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> AmountResourcesUsed([FromRoute]int meterId)
        {
            var meter = await _dbContext.Meters.FindAsync(meterId);

            if (meter == null)
            {
                return BadRequest("No such Meter found");
            }

            var readingsForCurrentMonth = _dbContext.MeterReadings
                .Where(mr => mr.DateTimeReading.Value.Month == DateTime.Today.Month && mr.DateTimeReading.Value.Year == DateTime.Today.Year && mr.MeterId == meter.MeterId)
                .Select(mr => mr.ReadingNumbers)
                .ToList();

            var readingFirstDay = readingsForCurrentMonth
                .Where(r => r == readingsForCurrentMonth.Min())
                .ToList()[0];

            var lastReading = readingsForCurrentMonth
                .Where(r => r == readingsForCurrentMonth.Max())
                .ToList()[0];

            var razn = lastReading - readingFirstDay;
            return Ok(razn);
        }

        [HttpGet]
        [Route("GetPriceForMonth/{meterId=int}/{monthNumber=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> GetPriceForMonth([FromRoute] int meterId, [FromRoute] int monthNumber)
        {
            double pricePerMonth = 0;
            var meter = await _dbContext.Meters.FindAsync(meterId);

            if (meter == null)
            {
                return NotFound("No such meter");
            }

            var startMeterReading = _dbContext.MeterReadings
                .Where(mr => mr.DateTimeReading!.Value.Month == monthNumber && mr.DateTimeReading.Value.Day == 1 && mr.MeterId == meter.MeterId)
                .Select(mr => mr.ReadingNumbers).ToList()[0];

            var monthMeterReadings = _dbContext.MeterReadings
                .Where(mr => mr.DateTimeReading!.Value.Month == monthNumber && mr.MeterId == meter.MeterId)
                .ToList();

            var endMeterReadings = monthMeterReadings
                .Where(mr => mr.DateTimeReading.Value.Day == monthMeterReadings.Max(mr => mr.DateTimeReading.Value.Day))
                .Select(mr => mr.ReadingNumbers)
                .ToList()[0];

            double price = (endMeterReadings - startMeterReading) * meter.ResourcePrice;
            return Ok(price);
        }

        [HttpGet]
        [Route("CurrentMoneyNeeded/{meterId=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]

        public async Task<IActionResult> CurrentMoneyNeeded([FromRoute] int meterId)
        {
            var meter = await _dbContext.Meters.FindAsync(meterId);

            if(meter == null)
            {
                return NotFound("No Such Meter");
            }

            var startMeterReading = _dbContext.MeterReadings
              .Where(mr => mr.DateTimeReading!.Value.Month == DateTime.Today.Month && mr.DateTimeReading.Value.Day == 1 && mr.MeterId == meter.MeterId)
              .Select(mr => mr.ReadingNumbers).ToList()[0];
            var endMeterReading = _dbContext.MeterReadings
                .Where(mr => mr.DateTimeReading.Value == _dbContext.MeterReadings.Max(mr => mr.DateTimeReading!.Value) && mr.MeterId == meter.MeterId)
                .Select(mr => mr.ReadingNumbers).ToList()[0];
            var currentMoney = (endMeterReading - startMeterReading) * meter.ResourcePrice;
            return Ok(currentMoney);
        }

        [HttpGet]
        [Route("MaximumAvailableMoney/{meterId=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]

        public async Task<IActionResult> MaximumAvailablaMoney([FromRoute] int meterId )
        {
            var meter = await _dbContext.Meters.FindAsync(meterId);

            if (meter == null)
            {
                return NotFound("No Such Meter");
            }

            var max = meter.MaximumAvailableValue * meter.ResourcePrice;
            return Ok(max);
        }
    }
}
