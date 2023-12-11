using Hotel_Project.Core;
using Hotel_Project.Models.Entities.Account;
using Hotel_Project.ViewModels.StoreProject;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hotel_Project.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreService _service;

        public StoreController(IStoreService service)
        {
            _service = service;
        }

        public IActionResult Shop()
        {
            return View(_service.ShowStore());
        }


        public IActionResult ShowSingleHotel(int id)
        {
            return View(_service.ShowSingleHotel(id));
        }

        public IActionResult ReserveRoom(int id)
        {
            return View(_service.ShowSingleRoom(id));
        }

        [HttpPost]
        public IActionResult CreateOrder(GetOrderVm viewModel)
        {
            viewModel.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            switch (_service.CreateOrder(viewModel))
            {
                case 0:
                    return RedirectToAction("Index", "Home");
                case 2:
                    return RedirectToAction("Index", "Home");
            }
            return RedirectToAction(nameof(UserBasket));
        }

        public IActionResult RemoveDetail(int id) 
        {
            switch (_service.RemoveDetail(id))
            {
                case 0:
                    return RedirectToAction("Index", "Home");
                case 1:
                    return RedirectToAction("Index", "Home");
            }
            return RedirectToAction(nameof(UserBasket));
        }


        public IActionResult UserBasket()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(_service.GetUserBasket(userId));
        }

        public IActionResult UserCheckout()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(_service.GetUserCheckout(userId));
        }

        public IActionResult Payment(CheckoutViewModel viewModel)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            switch (_service.PaymentOrder(userId ,viewModel))
            {
                case 0:
                    return RedirectToAction("Index", "Home");
                case 1:
                    return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("UserDashboard", "Account");
        }
    }
}
