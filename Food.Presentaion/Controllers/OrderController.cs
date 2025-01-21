using Microsoft.AspNetCore.Mvc;
using Food.Apllication.Services;
namespace Food.Presentaion.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllOrder(int PageNumber = 1, int Count= 10)
        {
            var data = await orderService.GetAllAsync(PageNumber, Count);
            return View(data);
        }
    }
}
