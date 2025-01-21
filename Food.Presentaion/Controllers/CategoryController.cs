using Food.Apllication.Services;
using Food.DTO.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Food.Presentaion.Controllers
{
    [Authorize(Roles="admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllCategory(int PageNumber=1,int Count = 10)
        {
            var data = await categoryService.GetAllAsync(PageNumber, Count);
            return View(data);
        }
        public async Task<IActionResult> Create()
        {

            CreateORupdateCategoryDTO categoryDTO = new();
            return View(categoryDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateORupdateCategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                var res = await categoryService.CreateAsync(categoryDTO);

                var msg = res.Message;
                TempData["Message"] = msg;
                return RedirectToAction("GetAllCategory");
            }
            return View();
        }
        public async Task<IActionResult>  Update(Guid Id)
        {
            var category = await categoryService.GetOneAsync(Id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CreateORupdateCategoryDTO categoryDTO)
        {  
            if (ModelState.IsValid)
            {
                var res = await categoryService.UpdateAsync(categoryDTO);
                var msg = res.Message;
                TempData["Messag"] = msg;
                return RedirectToAction("GetAllCategory");
            }
            return View();
        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await categoryService.DeleteAsync(Id);
            TempData["Message"] = result.Message;
            return RedirectToAction("GetAllCategory");
        }
    }
}
