using eFashionShop.Application.Orders;
using eFashionShop.Data.Entities;
using eFashionShop.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eFashionShop.Controllers.AdminController
{
    public class AdminOrderController : AdminBaseController
    {
        private readonly IOrderService _orderService;
        public AdminOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            if (keyword == null) keyword = "";
            var data = _orderService.GetAll(keyword);
            return View(data.Result);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = _orderService.Delete(id);
            if (result.IsCompleted)
            {
                TempData["result"] = "Xoá Thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["result"] = "Xoá thất bại";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null) throw new EShopException("Not found!");
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Order order)
        {
            if (order == null) throw new EShopException("Edit fail!");
            var result = _orderService.Update(order);
            if (result.IsCompleted)
            {
                TempData["result"] = "Cập nhập thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["result"] = "Cập nhập thất bại";
                return View(order);
            }
        }
    }
}
