using Books.DataAccessLayer.Infrastructure.IRepository;
using Books.DataAccessLayer.Infrastructure.Repository;
using Books.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitofwork;

        public CategoryController(IUnitOfWork unitofowrk)
        {
            _unitofwork = unitofowrk;
        }


        public IActionResult GetCategories()
        {
            var categories = _unitofwork.Categories.GetAll();
            return Json(new { data = categories });
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.Categories.Add(vm.Category);
                _unitofwork.Save();
                TempData["success"] = "Category Added Successfully";
                return RedirectToAction("Index");

            }

            return View(vm);

        }


        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            CategoryVM vm = new CategoryVM();
            vm.Category = _unitofwork.Categories.Get(x => x.CategoryId == id);
            return View(vm);
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryVM vm)
        {

            if (ModelState.IsValid)
            {
                _unitofwork.Categories.Update(vm.Category);
                _unitofwork.Save();
                TempData["success"] = "Category Updated";
                return RedirectToAction("Index");
            }

            return View(vm);
        }


        //[HttpGet]
        //public IActionResult DeleteCategory(int id)
        //{
        //    CategoryVM vm = new CategoryVM();
        //    vm.Category = _unitofwork.Categories.Get(x => x.CategoryId == id);
        //    return View(vm);
        //}


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var cat = _unitofwork.Categories.Get(x => x.CategoryId == id);

            if(cat != null)
            {
                _unitofwork.Categories.Delete(cat);
                _unitofwork.Save();
                return Json(new { success = true, message = "Category Deleted" });
            }
            else
            {
                return Json(new { success = false, message = "Category Not Found" });
            }
        }


    }
}
