using Microsoft.AspNetCore.Mvc;
using Food.Apllication.Services;
using Food.DTO.Product;
namespace Food.Presentaion.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        public ProductController(IProductService _productService,ICategoryService _categoryService)
        {
            productService = _productService;
            categoryService = _categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllProduct(int PageNumber=1,int Count=10)
        {
            var data = await productService.GetAllAsync(PageNumber,Count);
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            CreateORUpdateProductDTO productDTO = new();
            ViewBag.Categories = await categoryService.GetAllCategoryAsync();
            return View(productDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateORUpdateProductDTO entity)
        {
            if (ModelState.IsValid)
            {
                var res = await productService.CreateAsync(entity);
              
                var msg = res.Message;
                TempData["Messag"] = msg;
                return RedirectToAction("GetAllProduct");
            }
            return View();
        }
        public async Task<IActionResult> Update(Guid Id)
        {
            ViewBag.categories = await categoryService.GetAllCategoryAsync();
            var product = await productService.GetOneAsync(Id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CreateORUpdateProductDTO productDTO)
        {
			if (ModelState.IsValid)
			{
				var res = await productService.UpdateAsync(productDTO);

				var msg = res.Message;
				TempData["Messag"] = msg;
				return RedirectToAction("GetAllProduct");
			}
			return View();
		}
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await productService.DeleteAsync(Id);
            
            TempData["Message"] = result.Message;
            return RedirectToAction("GetAllProduct");    
           
        }
    }
}
