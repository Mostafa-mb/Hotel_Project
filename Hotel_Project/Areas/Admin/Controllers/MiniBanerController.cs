using Hotel_Project.Data;
using Hotel_Project.Models.Entities.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MiniBanerController : Controller
    {
        MyContext _context;
        public MiniBanerController(MyContext context)
        {
            _context = context;
        }

        public IActionResult MiniBaners()
        {
            return View(_context.fisrtBaners.ToList());
        }

        public IActionResult CreateBaner()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBaner(FisrtBaner baner)
        {
            if (ModelState.IsValid)
            {
                var newBaner = new FisrtBaner()
                {
                    BanerButton = baner.BanerButton,
                    BanerTitle = baner.BanerTitle,
                    ImageName = baner.ImageName
                };
                _context.fisrtBaners.Add(newBaner);
                _context.SaveChanges();
                return RedirectToAction(nameof(MiniBaners));
            }
            ModelState.AddModelError("ModelOnly","لطفا فیلدهارا کامل پر کنید");
            return View(baner);
        }

        public IActionResult EditBaner(int? id)
        {
            if(id != null)
            {
                var baner = _context.fisrtBaners.SingleOrDefault(b => b.Id == id);
                if(baner != null)
                {
                    return View(baner);
                }
                return RedirectToAction(nameof(MiniBaners));
            }
            return RedirectToAction(nameof(MiniBaners));
        }

        [HttpPost]
        public IActionResult EditBaner(int? id, FisrtBaner baner)
        {
            if (id == baner.Id)
            {
                if (ModelState.IsValid)
                {
                    var myBaner = _context.fisrtBaners.SingleOrDefault(b=>b.Id == id);

                    myBaner.BanerTitle = baner.BanerTitle;
                    myBaner.BanerButton = baner.BanerButton;
                    myBaner.ImageName = baner.ImageName;

                    _context.fisrtBaners.Update(myBaner);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(MiniBaners));
                }
                ModelState.AddModelError("ModelOnly", "لطفا فیلدهارا کامل پر کنید");
                return View(baner);
            }
            ModelState.AddModelError("ModelOnly", "لطفا فیلدهای موجود را کامل کنید");
            return View(baner);
        }

        public IActionResult RemoveBaner(int? id)
        {
            if (id != null)
            {
                var baner = _context.fisrtBaners.SingleOrDefault(b => b.Id == id);
                if (baner != null)
                {
                    return View(baner);
                }
                return RedirectToAction(nameof(MiniBaners));
            }
            return RedirectToAction(nameof(MiniBaners));
        }

        [HttpPost]
        public IActionResult RemoveBaner(int? id, FisrtBaner baner)
        {
            if (id == baner.Id)
            {
                
                    var myBaner = _context.fisrtBaners.SingleOrDefault(b => b.Id == id);

                    _context.fisrtBaners.Remove(myBaner);
                    _context.SaveChanges();

                return RedirectToAction(nameof(MiniBaners));
            }
            ModelState.AddModelError("ModelOnly", "لطفا فیلدهای موجود را کامل کنید");
            return View(baner);
        }
    }
}
