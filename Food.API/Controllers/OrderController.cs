using Food.Apllication.Services;
using Food.DTO.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Food.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService _orderService)
        {
            this.orderService = _orderService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await orderService.GetAll();
            return Ok(data);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateORUpdateOrderDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                var data = await orderService.CreateAsync(orderDTO);
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
        public  async Task<IActionResult> Update(CreateORUpdateOrderDTO orderDTO)
        {
            if (ModelState.IsValid)
            {
                var data = await orderService.UpdateAsync(orderDTO);
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
            var data = await orderService.DeleteAsync(Id);
            return Ok(data);
        }
    }
}
