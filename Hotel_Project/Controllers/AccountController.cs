using Hotel_Project.Core;
using Hotel_Project.Data;
using Hotel_Project.Models.Entities.Account;
using Hotel_Project.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hotel_Project.Controllers
{
    public class AccountController : Controller
    {
        MyContext _context;
        private readonly IStoreService _service;

        public AccountController(IStoreService service , MyContext context)
        {
            _context = context;
            _service = service;
        }

        #region Register
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("register"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (_context.users.Any(u => u.Email == register.Email))
                {
                    ModelState.AddModelError("Email", "ایمیل شما تکراری میباشد");
                    return View(register);
                }

                var user = new User
                {
                    Email = register.Email,
                    Password = register.Password
                };

                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction("login");
            }
            ModelState.AddModelError("ModelOnly", "لطفا فیلدهارا کامل پر کنید");
            return View(register);
        }
        #endregion

        #region Login

        [Route("/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/Login"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = _context.users.SingleOrDefault(u => u.Email == login.Email);
                if (user != null)
                {
                  
                    if (user.Password == login.Password)
                    {
                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                            new Claim(ClaimTypes.Email, login.Email)
                        };

                        var Identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var Principal = new ClaimsPrincipal(Identity);

                        var Properties = new AuthenticationProperties()
                        {
                            IsPersistent = login.IsRemeberMe
                        };

                        HttpContext.SignInAsync(Principal, Properties);
                        return RedirectToAction("UserDashboard", "Account");


                    }
                    ModelState.AddModelError("Email", "اطلاعات وارد شده صحیح نمیباشد");
                    return View();
                }
                ModelState.AddModelError("Email", "اطلاعات وارد شده صحیح نمیباشد");
                return View();
            }
            ModelState.AddModelError("Email", "لطفا فیلدهارا کامل پر کنید");
            return View();
        }

        [Route("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login");
        }

        #endregion

        #region Dashboard-User

        [Authorize]
        public IActionResult UserDashboard()
        {
            return View();
        }

        [Authorize]
        public IActionResult UserOrders()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(_service.UserOrders(userId));
        }

        #endregion

    }
}
