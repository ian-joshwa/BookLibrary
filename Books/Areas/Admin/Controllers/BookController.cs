using Books.DataAccessLayer.Infrastructure.IRepository;
using Books.Models;
using Books.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private IWebHostEnvironment _webHostEnvironment;

        public BookController(IUnitOfWork unitofwork,
            IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitofwork;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult GetBooks()
        {
            var books = _unitofwork.Books.GetAll(includeProperties: "Category");
            return Json(new {data=books});
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            BookVM vm = new BookVM();
            vm.Categories = _unitofwork.Categories.GetAll().Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = x.Name,
                Value = x.CategoryId.ToString()
            });
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddBook(BookVM vm, IFormFile? file)
        {

            if(file != null)
            {
                var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "BookImages");
                var filename = Guid.NewGuid().ToString() + "-" + file.FileName;
                var filePath = Path.Combine(uploadDir, filename);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(filestream);
                };
                vm.Book.Image = @"\BookImages\" + filename;
            }

            vm.Book.Status = true;
            if (ModelState.IsValid)
            {
                _unitofwork.Books.Add(vm.Book);
                _unitofwork.Save();
                TempData["success"] = "Book Added Successfully";
                return RedirectToAction("Index");

            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult EditBook(int id)
        {
            BookVM vm = new BookVM();
            vm.Book = _unitofwork.Books.Get(x => x.BookId == id);
            vm.Categories = _unitofwork.Categories.GetAll().Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = x.Name,
                Value = x.CategoryId.ToString()
            });
            return View(vm);
        }

        [HttpPost]
        public IActionResult EditBook(BookVM vm, IFormFile? file)
        {

            if(file != null)
            {

                var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "BookImages");
                var filename = Guid.NewGuid().ToString() + "-" + file.FileName;
                var filePath = Path.Combine(uploadDir, filename);

                if (vm.Book.Image != null)
                {

                    var oldimage = Path.Combine(_webHostEnvironment.WebRootPath, vm.Book.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(oldimage))
                    {
                        System.IO.File.Delete(oldimage);
                    }

                }

                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(filestream);
                };
                vm.Book.Image = @"\BookImages\" + filename;

            }

            if (ModelState.IsValid)
            {
                _unitofwork.Books.Update(vm.Book);
                _unitofwork.Save();
                TempData["success"] = "Book Updated";
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        //[HttpGet]
        //public IActionResult DeleteBook(int id)
        //{
        //    BookVM vm = new BookVM();
        //    vm.Book = _unitofwork.Books.Get(x => x.BookId == id);
        //    vm.Book.Category = _unitofwork.Categories.Get(x => x.CategoryId == vm.Book.CategoryId);
        //    return View(vm);
        //}


        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var bk = _unitofwork.Books.Get(x => x.BookId == id);

            if(bk != null)
            {
                var imagepath = Path.Combine(_webHostEnvironment.WebRootPath, bk.Image.TrimStart('\\'));
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }

                _unitofwork.Books.Delete(bk);
                _unitofwork.Save();
                return Json(new { success = true, message = "Book Deleted" });

            }
            else
            {
                return Json(new { success = false, message = "Book Not Found" });
            }
        }
    }
}
