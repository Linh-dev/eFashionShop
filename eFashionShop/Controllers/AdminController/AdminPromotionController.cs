using eFashionShop.Application.Promotions;
using eFashionShop.ViewModels.Catalog.Promotions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace eFashionShop.Controllers.AdminController
{
    public class AdminPromotionController : AdminBaseController
    {
        private readonly IPromotionService _promotionService;
        public AdminPromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }
        public async Task<IActionResult> Index()
        {
            var data = _promotionService.GetAll();
            return View(data.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PromotionCreateVm res)
        {
            if (!ModelState.IsValid) return View(res);
            var result = _promotionService.Create(res);
            if (result.IsCompleted)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Thêm mới thất bại");
            return View(res);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = _promotionService.Delete(id);
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
    }
}
