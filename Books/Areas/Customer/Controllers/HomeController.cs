using Books.DataAccessLayer.Data;
using Books.DataAccessLayer.Infrastructure.IRepository;
using Books.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Books.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult Index()
        {

            IEnumerable<Book> books = _unitOfWork.Books.GetAll(includeProperties: "Category");
            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
