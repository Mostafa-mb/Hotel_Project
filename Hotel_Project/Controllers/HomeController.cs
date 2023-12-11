using Hotel_Project.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var baners = _context.fisrtBaners.ToList();
            TempData["mainPage"] = "MianPage";
            return View(baners);
        }
    }
}
