using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly OrderMapperService _mapperService;

        public OrderController(ApplicationDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapperService = new OrderMapperService(mapper);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await  _dbContext.Orders.Include(o => o.Renter).Include(o => o.Warehouse).ToListAsync(); //
            var orderDtos = _mapperService.AsDtoList(orders);
            return Ok(orderDtos);
        }

        [HttpGet]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var order = await _dbContext.Orders.Where(o=>o.OrderId == id).Include(o => o.Warehouse).Include(o=>o.Renter).FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            var dto = _mapperService.AsDto(order);
            return Ok(dto); 
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Create(OrderDto orderDto)
        {
            var order = _mapperService.AsModel(orderDto);
            var renter = await _dbContext.Renters.FindAsync(orderDto.RenterId);

            if (renter == null)
            {
                return NotFound();
            }

            var warehouse = await _dbContext.Warehouses.FindAsync(orderDto.WarehouseId);

            if(warehouse == null)
            {
                return NotFound();
            }

            order.Warehouse = warehouse;
            order.Renter = renter;
            orderDto = _mapperService.AsDto(order);
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return Ok(orderDto);
        }

        [HttpDelete]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);

            if(order == null)
            {
                return NotFound();
            }

            var orderDto = _mapperService.AsDto(order);
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return Ok(orderDto);
        }

        [HttpPut]
        [Route("{id=int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "General, Admin")]
        public async Task<IActionResult> Update([FromRoute]int id, OrderDtoUpdate updateDto)
        {
            var order = await _dbContext.Orders.FindAsync(id);

            if(order == null)
            {
                return NotFound();
            }

            OrderUpdateServcie updateServcie = new OrderUpdateServcie();
            updateServcie.Update(order, updateDto);
            var renter = await _dbContext.Renters.FindAsync(order.RenterId);

            if(renter == null)
            {
                return NotFound();
            }

            var warehouse = await _dbContext.Warehouses.FindAsync(order.WarehouseId);

            if(warehouse == null)
            {
                return NotFound();
            }

            order.Warehouse = warehouse;
            order.Renter = renter;
            var orderDto = _mapperService.AsDto(order);
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
            return Ok(orderDto);
        }
    }
}
