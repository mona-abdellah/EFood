using Food.Apllication.Contracts;
using Food.Apllication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Food.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategory(int PageNumber, int Count)
        {
            var data = await categoryService.GetAllAsync(PageNumber, Count);
            return Ok(data);
        }
    }
}
