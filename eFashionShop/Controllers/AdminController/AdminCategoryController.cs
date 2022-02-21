using eFashionShop.Application.Categories;
using eFashionShop.ViewModels.Catalog.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eFashionShop.Controllers.AdminController
{
    public class AdminCategoryController : AdminBaseController
    {
        private readonly ICategoryService _categoryService;
        public AdminCategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var data = _categoryService.GetAll();
            return View(data.Result);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = _categoryService.Delete(id);
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
        public async Task<IActionResult> Create()
        {
            var ListCategoryParent = _categoryService.GetListParent();
            ViewBag.ListCategoryParent = ListCategoryParent.Result;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVm res)
        {
            var ListCategoryParent = _categoryService.GetListParent();
            if (!ModelState.IsValid)
            {
                ViewBag.ListCategoryParent = ListCategoryParent.Result;
                return View(res);
            }

            var result = _categoryService.Create(res);
            if (result.Result)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Thêm mới thất bại");
            ViewBag.ListCategoryParent = ListCategoryParent.Result;
            return View(res);
        }


    }
}
