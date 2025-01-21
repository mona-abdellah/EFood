using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Food.Apllication.Services;
namespace Food.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            this.productService = _productService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int PageNumber,int Count)
        {
            var data = await productService.GetAllAsync(PageNumber, Count);
            return Ok(data);
        }
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProduct(Guid CatId,int PageNumber,int Count)
        {
            var data = await productService.GetAllByCatId(CatId, PageNumber, Count);
            return Ok(data);
        }
    }
}
