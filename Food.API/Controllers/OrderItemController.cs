using Food.Apllication.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Food.Apllication.Services;
using Food.DTO.Order;
namespace Food.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrederItemService orderItemService;
        public OrderItemController(IOrederItemService _orderItemService)
        {
            orderItemService = _orderItemService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await orderItemService.GetAllAsync();
            return Ok(data);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateORUpdateOrderItemDTO orderItemDTO)
        {
            if (ModelState.IsValid)
            {
                var data = await orderItemService.CreateAsync(orderItemDTO);
                if (data.ISSuccess)
                {
                    return Ok(data.Message);
                }
                else
                {
                    return BadRequest(data.Message);
                }
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> Update(CreateORUpdateOrderItemDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                var data = await orderItemService.UpdateAsync(orderDTO);
                if (data.ISSuccess)
                {
                    return Ok(data.Message);
                }
                else
                {
                    return Ok(data.Message);
                }
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var data = await orderItemService.DeleteAsync(Id);
            return Ok(data);
        }
    }
}
