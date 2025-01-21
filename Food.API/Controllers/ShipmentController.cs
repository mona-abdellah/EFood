using Food.Apllication.Services;
using Food.DTO.Shipment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Food.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService shipmentService;
        public ShipmentController(IShipmentService _shipmentService)
        {
            shipmentService = _shipmentService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAndUpdateShipmentDTO shipmentDTO)
        {
            if (ModelState.IsValid)
            {
                var data = await shipmentService.CreateAsync(shipmentDTO);
                if (data.ISSuccess)
                {
                    return Ok(data.Message);
                }
                return BadRequest(data.Message);
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> Update(CreateAndUpdateShipmentDTO shipmentDTO)
        {
            if (ModelState.IsValid)
            {
                var data = await shipmentService.UpdateAsync(shipmentDTO);
                if (data.ISSuccess)
                {
                    return Ok(data.Message);
                }
                return BadRequest(data.Message);
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var data = await shipmentService.DeleteAsync(Id);
            return Ok(data);
        }
    }
}
