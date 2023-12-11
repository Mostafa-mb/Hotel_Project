using Hotel_Project.Core;
using Hotel_Project.Models.Product;
using Hotel_Project.ViewModels.Product.Advantage;
using Hotel_Project.ViewModels.Product.Hotel;
using Hotel_Project.ViewModels.Product.HotelImages;
using Hotel_Project.ViewModels.Product.HotelRulesVm;
using Hotel_Project.ViewModels.Product.RoomReserveDate;
using Hotel_Project.ViewModels.Product.Rooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private IHotelService _service;

        public ProductController(IHotelService service)
        {
            _service = service;
        }

        #region BaseHotel
        public IActionResult ShowAllHotels()
        {
            return View(_service.GetAllHotels());
        }

        public IActionResult CreateHotel()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateHotel(CreateHotelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var hotel = new Hotel()
                    {
                        Title = viewModel.Title,
                        dateTime = DateTime.Now,
                        Description = viewModel.Description,
                        EntryTime = viewModel.EntryTime,
                        ExitTime = viewModel.ExitTime,
                        RoomCount = viewModel.RoomCount,
                        StageCount = viewModel.StageCount
                    };

                    _service.InsertHotel(hotel);
                    _service.SaveChange();

                    var address = new HotelAddress()
                    {
                        Address = viewModel.Address,
                        City = viewModel.City,
                        State = viewModel.State,
                        PostalCode = viewModel.PostalCode,
                        HotelId = hotel.Id
                    };
                    _service.InsertAddress(address);
                    hotel.IsActive = true;
                    _service.SaveChange();

                    return RedirectToAction("ShowAllHotels");

                }
                catch (Exception)
                {

                    ModelState.AddModelError(nameof(viewModel.Title), "عملیات با خطا مواجه شد لطفا مجددا تلاش کنید");
                    return View(viewModel);
                }
            }
            ModelState.AddModelError(nameof(viewModel.Title), "لطفا تمامی فیلد هارا پر کنید");
            return View(viewModel);
        }

        public IActionResult EditHotel(int id)
        {
            var vm = _service.GetHotelViewModel(id);
            if (vm != null)
            {
                return View(vm);
            }
            return RedirectToAction("ShowAllHotels");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditHotel(int id, EditHotelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (id == viewModel.Id)
                {
                    var hotel = _service.GetHotelById(id);
                    if (hotel != null)
                    {
                        hotel.Title = viewModel.Title;
                        hotel.ExitTime = viewModel.ExitTime;
                        hotel.ExitTime = viewModel.EntryTime;
                        hotel.Description = viewModel.Description;
                        hotel.IsActive = viewModel.IsActive;
                        hotel.RoomCount = viewModel.RoomCount;
                        hotel.StageCount = viewModel.StageCount;

                        var myAddress = hotel.hotelAddress;
                        myAddress.Address = viewModel.Address;
                        myAddress.PostalCode = viewModel.PostalCode;
                        myAddress.City = viewModel.City;
                        myAddress.State = viewModel.State;

                        _service.UpdateHotel(hotel);
                        _service.UpdateAddress(myAddress);
                        _service.SaveChange();

                        return RedirectToAction("ShowAllHotels");
                    }
                    return RedirectToAction("ShowAllHotels");
                }
                ModelState.AddModelError(nameof(viewModel.Title), "لطفا تمامی فیلد هارا پر کنید");
                return View(viewModel);
            }
            ModelState.AddModelError(nameof(viewModel.Title), "لطفا موارد ارسالی را مجددا چک کنید");
            return View(viewModel);
        }

        public IActionResult RemoveHotel(int id)
        {
            var vm = _service.GetHotelViewModel(id);
            if (vm != null)
            {
                return View(vm);
            }
            return RedirectToAction("ShowAllHotels");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult RemoveHotel(int? id)
        {
            if (id != null)
            {
                var hotel = _service.GetHotelById(id.Value);
                if (hotel != null)
                {
                    _service.RemoveHotel(hotel);
                    _service.RemoveAddress(hotel.hotelAddress);
                    _service.SaveChange();
                    return RedirectToAction("ShowAllHotels");
                }
                return RedirectToAction("ShowAllHotels");
            }
            return RedirectToAction("ShowAllHotels");
        }

        #endregion

        #region Hotelgallery

        public IActionResult ShowAllHotelImage(int id)
        {
            return View(new ShowHotelImagesVM() { Id = id, HotelGalleries = _service.HotelGalleries(id) });
        }

        public IActionResult InsertImageToHotel(int id)
        {
            return View(new InsertAndRemoveImageVm() { Id = id });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult InsertImageToHotel(InsertAndRemoveImageVm viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.File != null)
                {
                    string imgName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(viewModel.File.FileName);
                    string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/asset/img/HotelImages", imgName);

                    using (var strem = new FileStream(imgPath, FileMode.Create))
                    {
                        viewModel.File.CopyTo(strem);
                    }

                    var image = new HotelGallery() { HotelId = viewModel.Id, ImageName = imgName };
                    _service.AddHotelGallery(image);
                    _service.SaveChange();
                    return RedirectToAction("ShowAllHotelImage", new { id = viewModel.Id });


                }
                ModelState.AddModelError(nameof(viewModel.File), "لطفا تصویر را وارد کنید");
                return View();
            }
            return View();
        }

        public IActionResult RemoveHotelImage(int id)
        {
            return View(new InsertAndRemoveImageVm() { Id = id, ImageName = _service.GetGalleryById(id).ImageName });
        }


        public IActionResult RemoveHotelfromImage(int? id)
        {
            if (id != null)
            {
                var image = _service.GetGalleryById(id.Value);
                if (image != null)
                {
                    if (_service.RemoveImage(image.ImageName))
                    {
                        _service.RemoveGallery(image);
                        _service.SaveChange();
                        return RedirectToAction("ShowAllHotelImage", new { id = image.HotelId });
                    }
                    else
                    {
                        return View(new InsertAndRemoveImageVm() { Id = id.Value, ImageName = _service.GetGalleryById(id.Value).ImageName });
                    }
                }
                else
                {
                    return View("ShowAllHotels");
                }
            }
            return View("ShowAllHotels");
        }
        #endregion

        #region HotelRule

        public IActionResult ShowAllRules(int id)
        {
            ViewBag.HotelId = id;
            return View(_service.ShowallHotelRules(id));
        }

        public IActionResult InsertRuleToHotel(int id)
        {
            ViewBag.HotelId = id;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult InsertRuleToHotel(CrudForHotelRuleVm viewModel)
        {
            if (ModelState.IsValid)
            {
                _service.InsertRuleToHotel(new HotelRule() { HotelId = viewModel.Id, Description = viewModel.Description });
                _service.SaveChange();
                return RedirectToAction("ShowAllRules", new { id = viewModel.Id });
            }
            ModelState.AddModelError(nameof(viewModel.Description), "لطفا تمامی فیلد هارا پر کنید");
            ViewBag.HotelId = viewModel.Id;
            return View(viewModel);
        }

        public IActionResult EditHotelRule(int id)
        {
            return View(_service.GetRuleViewModel(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditHotelRule(CrudForHotelRuleVm viewModel)
        {
            var rule = _service.FindHotelRule(viewModel.Id);
            if (ModelState.IsValid)
            {
                rule.Description = viewModel.Description;
                _service.UpdateHotelRule(rule);
                _service.SaveChange();

                return RedirectToAction("ShowAllRules", new { id = rule.HotelId });
            }
            ModelState.AddModelError(nameof(viewModel.Description), "لطفا تمامی فیلد هارا پر کنید");
            return View(viewModel);
        }

        public IActionResult RemoveHotelRule(int id)
        {
            return View(_service.GetRuleViewModel(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult RemoveHotelRule(CrudForHotelRuleVm viewModel)
        {
            var rule = _service.FindHotelRule(viewModel.Id);
            if (ModelState.IsValid)
            {
                _service.RemoveHotelRule(rule);
                _service.SaveChange();

                return RedirectToAction("ShowAllRules", new { id = rule.HotelId });
            }
            ModelState.AddModelError(nameof(viewModel.Description), "لطفا تمامی فیلد هارا پر کنید");
            return View(viewModel);
        }
        #endregion

        #region HotelRoom

        public IActionResult ShowAllRooms(int id)
        {
            return View(_service.ShowAllHotelRoom(id));
        }

        public IActionResult CreateRoom(int id)
        {
            return View(new CreateRoomViewModel() { HotelId = id });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateRoom(CreateRoomViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.File != null)
                {
                    try
                    {
                        string imgName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(viewModel.File.FileName);
                        string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/asset/img/RoomHotel", imgName);

                        using (var strem = new FileStream(imgPath, FileMode.Create))
                        {
                            viewModel.File.CopyTo(strem);
                        }
                        _service.InsertRoomToHotel(new HotelRoom()
                        {
                            HotelId = viewModel.HotelId,
                            BedCount = viewModel.BedCount,
                            Capacity = viewModel.Capacity,
                            Count = viewModel.Count,
                            Description = viewModel.Description,
                            RoomPrice = viewModel.RoomPrice,
                            Title = viewModel.Title,
                            ImageName = imgName
                        });
                        _service.SaveChange();
                        return RedirectToAction(nameof(ShowAllRooms), new { id = viewModel.HotelId });
                    }
                    catch
                    {
                        ModelState.AddModelError(nameof(viewModel.File), "لطفا مجددا تصویر را وارد کنید");
                        return View(viewModel);
                    }

                }
                ModelState.AddModelError(nameof(viewModel.File), "لطفا تصویر را وارد کنید");
                return View(viewModel);
            }
            ModelState.AddModelError(nameof(viewModel.Title), "لطفا تمامی فیلد هارا پر کنید");
            return View(viewModel);
        }

        public IActionResult EditHotelRoom(int id)
        {
            return View(_service.GetHotelRoom(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditHotelRoom(UpdateAndRemoveRoomVm viewModel)
        {
            if (ModelState.IsValid)
            {
                switch (_service.UpdateHotelRoom(viewModel))
                {
                    case 0:
                        return RedirectToAction("ShowAllHotels");
                    case 1:
                        ModelState.AddModelError(nameof(viewModel.Title), "لطفا مجددا تلاش کنید");
                        return View(viewModel);
                    case 2:
                        ModelState.AddModelError(nameof(viewModel.IsActive), "لطفا ابتدا تاریخ رزرو را بروز کنید سپس مجددا تلاش کنید");
                        return View(viewModel);
                    case 3:
                        return RedirectToAction("ShowAllRooms", new { id = viewModel.HotelId });
                }

            }
            ModelState.AddModelError(nameof(viewModel.Title), "لطفا تمامی فیلد هارا پر کنید");
            return View(viewModel);
        }

        public IActionResult RemoveHotelRoom(int id)
        {
            return View(_service.GetHotelRoom(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult RemoveHotelRoom(UpdateAndRemoveRoomVm viewModel)
        {
            switch (_service.DeleteHotelRoom(viewModel.Id))
            {
                case 2:
                    return RedirectToAction("ShowAllHotels");
                case 0:
                    ModelState.AddModelError(nameof(viewModel.Title), "لطفا مجددا تلاش کنید");
                    return View(viewModel);
                case 1:
                    return RedirectToAction("ShowAllRooms", new { id = viewModel.HotelId });
            }
            return View(_service.GetHotelRoom(viewModel.Id));
        }
        #endregion

        #region Advantage

        public IActionResult ShowAllAdvantage()
        {
            return View(_service.ShowAllAdvantage());
        }

        public IActionResult CreateAdvantage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAdvantage(CrudForAdvantageVm viewModel)
        {
            if (ModelState.IsValid)
            {
                _service.CreateAdvantage(new AdvantageRoom()
                {
                    Name = viewModel.Name
                });
                _service.SaveChange();
                return RedirectToAction("ShowAllAdvantage");
            }
            ModelState.AddModelError(nameof(viewModel.Name), "لطفا تمامی فیلد هارا پر کنید");
            return View(viewModel);
        }



        public IActionResult UpdateAdvantage(int id)
        {
            var advantage = _service.FindAdvantageRoom(id);
            if (advantage != null)
            {
                return View(new CrudForAdvantageVm { Name = advantage.Name });

            }
            return RedirectToAction("ShowAllAdvantage");
        }

        [HttpPost]
        public IActionResult UpdateAdvantage(CrudForAdvantageVm viewModel)
        {
            if (ModelState.IsValid)
            {
                var advantage = _service.FindAdvantageRoom(viewModel.Id);
                if (advantage != null)
                {
                    advantage.Name = viewModel.Name;
                    _service.UpdateAdvantage(advantage);
                    _service.SaveChange();
                    return RedirectToAction("ShowAllAdvantage");
                }
                return RedirectToAction("ShowAllAdvantage");
            }
            ModelState.AddModelError(nameof(viewModel.Name), "لطفا تمامی فیلد هارا پر کنید");
            return RedirectToAction("ShowAllAdvantage");
        }



        public IActionResult DeleteAdvantage(int id)
        {
            var advantage = _service.FindAdvantageRoom(id);
            if (advantage != null)
            {
                return View(new CrudForAdvantageVm { Name = advantage.Name });

            }
            return RedirectToAction("ShowAllAdvantage");
        }

        [HttpPost]
        public IActionResult DeleteAdvantage(CrudForAdvantageVm viewModel)
        {
            var advantage = _service.FindAdvantageRoom(viewModel.Id);
            if (advantage != null)
            {
                _service.RemoveAdvantage(advantage);
                _service.SaveChange();
                return RedirectToAction("ShowAllAdvantage");
            }
            return RedirectToAction("ShowAllAdvantage");
        }
        #endregion

        #region AdvantageForRoom

        public IActionResult ShowAdvantagesRoom(int id)
        {
            return View(_service.ShowAdvantagesRoomVm(id));
        }

        public IActionResult InsertAdvantageToRoom(int id)
        {
            return View(_service.GetAdvantagesForInsert(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult InsertAdvantageToRoom(InsertAdvantagesToRoomVm viewModel)
        {

            _service.RemoveAdvantagesForRoom(viewModel.RoomId);

            if (viewModel.AdvantagesId != null)
            {
                var advantages = new List<AdvantageToRoom>();
                foreach (var item in viewModel.AdvantagesId)
                {
                    advantages.Add(new AdvantageToRoom()
                    {
                        AdvantageId = item,
                        RoomId = viewModel.RoomId
                    });
                }
                _service.InsertAdvantageToRoom(advantages);
                _service.SaveChange();
            }
            return RedirectToAction("ShowAdvantagesRoom", new { id = viewModel.RoomId });
         
        }

        #endregion

        #region ReserveDate

        public IActionResult ShowAllReserveDates(int id)
        {
            return View(_service.ShowAllReserveDate(id));
        }

        public IActionResult InsertReserveDateToRoom(int id)
        {
            return View(_service.GetInsertRDtoRoom(id));
        }

        [HttpPost , ValidateAntiForgeryToken]
        public IActionResult InsertReserveDateToRoom(InsertReserveDateVm viewModel)
        {
            switch (_service.InsertRDtoRoom(viewModel))
            {
                case 0:
                    break;
                case 1:
                    return RedirectToAction("ShowAllReserveDates" , new {id = viewModel.roomId });
            }

            return View(_service.GetInsertRDtoRoom(viewModel.roomId));
        }

        public IActionResult UpdateReserveDate(int id)
        {
            return View(_service.GetReserveDateForUpdate(id));
        }

        [HttpPost , ValidateAntiForgeryToken]
        public IActionResult UpdateReserveDate(UpdateReserveDateVm vireModel)
        {
            var date = _service.GetReserveDate(vireModel.Id);
            if (ModelState.IsValid)
            {
                date.IsReserve = vireModel.IsReserve;
                date.Price = vireModel.Price;
                date.Count = vireModel.Count;
                _service.UpdateReserveDate(date);
                _service.SaveChange();
                return RedirectToAction("ShowAllReserveDates", new { id = date.RoomId });

            }
            return RedirectToAction("ShowAllReserveDates" , new {id = date.RoomId});
        }

        public IActionResult DeleteReserveDate(int id)
        {
            return View(_service.GetReserveDateForUpdate(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteReserveDate(UpdateReserveDateVm vireModel)
        {
            var date = _service.GetReserveDate(vireModel.Id);
            _service.RemoveReserveDate(date);
            _service.SaveChange();
            return RedirectToAction("ShowAllReserveDates", new { id = date.RoomId });
        }
        #endregion
    }
}
