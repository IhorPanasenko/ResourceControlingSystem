using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly AdminMapperService _mapperService;

        public AdminController(ApplicationDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _mapperService = new AdminMapperService(mapper);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var admins = await _dbContext.Admins.ToListAsync();
            var adminDtos = _mapperService.AsDtoList(admins);
            return Ok(adminDtos);
        }

        [HttpGet]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var admin = await _dbContext.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            var adminDto = _mapperService.AsDto(admin);
            return Ok(adminDto);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Create(AdminDto adminDto)
        {
            if (adminDto == null)
            {
                return BadRequest();
            }

            var admin = _mapperService.AsModel(adminDto);
            await _dbContext.Admins.AddAsync(admin);
            await _dbContext.SaveChangesAsync();
            return Ok(adminDto);
        }

        [HttpDelete]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var admin = await _dbContext.Admins.FindAsync(id);
            
            if(admin == null)
            {
                return NotFound();
            }

            var adminDto = _mapperService.AsDto(admin);
            _dbContext.Admins.Remove(admin);
            await _dbContext.SaveChangesAsync();
            return Ok(adminDto);
        }

        [HttpPut]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, AdminDtoUpdate dtoUpdate)
        {
            var admin = await _dbContext.Admins.FindAsync(id);

            if(admin == null)
            {
                return NotFound();
            }

            AdminUpdateService updateService = new AdminUpdateService();
            updateService.Update(admin, dtoUpdate);
            var adminDto = _mapperService.AsDto(admin);
            _dbContext.Admins.Update(admin);
            await _dbContext.SaveChangesAsync();
            return Ok(adminDto);
        }
        [HttpPost("Login")]
        public IActionResult Login(UserLogin login)
        {
            try
            {
               var user = _dbContext.Admins.FirstOrDefault(r => r.Login == login.Login && r.Password == login.Password);
                if (user == null)
                {
                    return NotFound();
                }

                var tokenString = getToken(user);

                return Ok(tokenString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return BadRequest();
            }
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, login.Login) };

        }

        private string getToken(Admin user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Login!),
                new Claim(ClaimTypes.Email, user.EmailAddress!),
                new Claim(ClaimTypes.GivenName, user.FirstName!),
                new Claim(ClaimTypes.Surname, user.SecondName!),
                new Claim(ClaimTypes.Role, "Admin")
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
