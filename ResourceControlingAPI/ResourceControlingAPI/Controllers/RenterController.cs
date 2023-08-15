using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ResourceControlingAPI.Data;
using ResourceControlingAPI.Dtos;
using ResourceControlingAPI.MapperServices;
using ResourceControlingAPI.Models;
using ResourceControlingAPI.Services;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ResourceControlingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RenterController : Controller
    {
        private readonly RenterMapperService _mapperService;
        private readonly AddressMapperService _addressMapperService;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public RenterController(IMapper mapper, ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _mapperService = new RenterMapperService(mapper);
            _addressMapperService = new AddressMapperService(mapper);
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var renters = await _dbContext.Renters.Include(r => r.Address).ToListAsync();
            var renterDtos = _mapperService.AsDtoList(renters);
            return Ok(renterDtos);
        }

        [HttpGet]
        [Route("{id=int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public IActionResult Get([FromRoute] int id)
        {
            var renter = _dbContext.Renters.Where(r => r.RenterID == id).Include(r => r.Address).ToList().FirstOrDefault();

            if (renter == null)
            {
                return NotFound();
            }

            var renterDto = _mapperService.AsDto(renter);
            return Ok(renterDto);
        }

        [HttpPost("Register")]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Create([FromBody]RenterDto renterDto)
        {
            var address = _dbContext.Addresses.Where(a => a.AddressId == _dbContext.Addresses.Max(a => a.AddressId)).ToList()[0];
            if(address == null)
            {
                return BadRequest();
            }

            var renter = _mapperService.AsModel(renterDto);
            renter.Address = address;
            renter.AddressId = address.AddressId;

            await _dbContext.AddAsync(renter);
            await _dbContext.SaveChangesAsync();
            var tokenString = getToken(renter);
            return Ok(tokenString);
        }

        [HttpDelete]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var renter = await _dbContext.Renters.FindAsync(id);

            if (renter == null)
            {
                return NotFound("Invalid Renter Id");
            }

            var renterDto = _mapperService.AsDto(renter);
            _dbContext.Renters.Remove(renter);
            await _dbContext.SaveChangesAsync();
            return Ok(renterDto);
        }

        [HttpPut]
        [Route("{id=int}")]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, RenterDtoUpdate renterDto)
        {
            var renter = await _dbContext.Renters.FindAsync(id);

            if (renter == null)
            {
                return NotFound("Invalid Renter Id");
            }

            RenterUpdateService renterUpdateService = new RenterUpdateService();
            renterUpdateService.Update(renter, renterDto);
            var address = await _dbContext.Addresses.FindAsync(renter.AddressId);

            if (address == null)
            {
                return NotFound();
            }

            renter.Address = address;
            _dbContext.Renters.Update(renter);
            await _dbContext.SaveChangesAsync();
            var renterDtoRet = _mapperService.AsDto(renter);
            return Ok(renterDtoRet);
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLogin login)
        {
            var user = _dbContext.Renters.Include(r => r.Address).FirstOrDefault(r => r.Login == login.Login && r.Password == login.Password);
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, login.Login) };

            if (user == null)
            {
                return NotFound();
            }

            var tokenString = getToken(user);

            return Ok(tokenString);

        }

        private string getToken(Renter user)
        {
            string strId = user.RenterID.ToString();
            string strAddressId = user.AddressId.ToString();

            var claims = new[]
            {
                new Claim("renterId", strId),
                new Claim("addressId", strAddressId),
                new Claim("userLogin", user.Login!),
                new Claim("emailAddress", user.EmailAddress!),
                new Claim("firstName", user.FirstName!),
                new Claim("secondName", user.SecondName!),
                new Claim("role", "General")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

    }
}
