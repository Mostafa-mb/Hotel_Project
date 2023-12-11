using Hotel_Project.Data;
using Hotel_Project.Extention;
using Hotel_Project.Models.Product;
using Hotel_Project.ViewModels.Product.Advantage;
using Hotel_Project.ViewModels.Product.Hotel;
using Hotel_Project.ViewModels.Product.HotelRulesVm;
using Hotel_Project.ViewModels.Product.RoomReserveDate;
using Hotel_Project.ViewModels.Product.Rooms;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;

namespace Hotel_Project.Core
{

    public class HotelService : IHotelService
    {
        private MyContext _context;

        public HotelService(MyContext context)
        {
            _context = context;
        }
        #region BaseHotel

        public IEnumerable<Hotel> GetAllHotels()
        {
            return _context.hotels.ToList();
        }

        public Hotel GetHotelById(int id)
        {
            return _context.hotels.Include(a => a.hotelAddress).SingleOrDefault(h => h.Id == id) ?? throw new Exception();
        }
        public void UpdateAddress(HotelAddress address)
        {
            _context.hotelAddresses.Update(address);
        }

        public void UpdateHotel(Hotel hotel)
        {
            _context.hotels.Update(hotel);
        }

        public EditHotelViewModel GetHotelViewModel(int id)
        {
            return _context.hotels.Include(a => a.hotelAddress).Where(h => h.Id == id)
                .Select(eh => new EditHotelViewModel
                {
                    Id = eh.Id,
                    Title = eh.Title,
                    Description = eh.Description,
                    EntryTime = eh.EntryTime,
                    ExitTime = eh.ExitTime,
                    IsActive = eh.IsActive,
                    RoomCount = eh.RoomCount,
                    StageCount = eh.StageCount,
                    Address = eh.hotelAddress.Address,
                    City = eh.hotelAddress.City,
                    State = eh.hotelAddress.State,
                    PostalCode = eh.hotelAddress.PostalCode

                }).SingleOrDefault() ?? throw new Exception();

        }

        public void InsertAddress(HotelAddress address)
        {
            _context.hotelAddresses.Add(address);
        }

        public void InsertHotel(Hotel hotel)
        {
            _context.hotels.Add(hotel);
        }

        public void RemoveHotel(Hotel hotel)
        {
            _context.hotels.Remove(hotel);
        }

        public void RemoveAddress(HotelAddress address)
        {
            _context.hotelAddresses.Remove(address);
        }

        #endregion

        #region HotelImages

        public IEnumerable<HotelGallery> HotelGalleries(int id)
        {
            return _context.hotelGalleries.Where(g => g.HotelId == id).ToList();
        }

        public void AddHotelGallery(HotelGallery hotelGallery)
        {
            _context.hotelGalleries.Add(hotelGallery);
        }

        public HotelGallery GetGalleryById(int id)
        {
            return _context.hotelGalleries.Find(id) ?? throw new Exception();
        }

        public void RemoveGallery(HotelGallery gallery)
        {
            _context.hotelGalleries.Remove(gallery);
        }

        public bool RemoveImage(string ImageName)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/asset/img/HotelImages", ImageName);
                File.Delete(path);
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion

        #region HotelRules

        public IEnumerable<HotelRule> ShowallHotelRules(int id)
        {
            return _context.hotelRules.Where(r => r.HotelId == id).ToList();
        }

        public void InsertRuleToHotel(HotelRule rule)
        {
            _context.hotelRules.Add(rule);
        }

        public HotelRule FindHotelRule(int id)
        {
            return _context.hotelRules.Find(id) ?? throw new Exception();
        }

        public CrudForHotelRuleVm GetRuleViewModel(int id)
        {
            var rule = FindHotelRule(id);
            return new CrudForHotelRuleVm()
            {
                Id = rule.Id,
                Description = rule.Description
            };
        }

        public void UpdateHotelRule(HotelRule rule)
        {
            _context.hotelRules.Update(rule);
        }

        public void RemoveHotelRule(HotelRule rule)
        {
            _context.hotelRules.Remove(rule);
        }
        #endregion

        #region HotelRoom

        public ShowAllRoomsViewModel ShowAllHotelRoom(int id)
        {
            var rooms = _context.hotelRooms.Include(d => d.reserveDates).Include(a => a.advantageToRooms).AsQueryable()
                .Where(r => r.HotelId == id);

            var showRoom = rooms.Select(r => new ShowSingleRoomVm()
            {
                Id = r.Id,
                Title = r.Title,
                BedCount = r.BedCount,
                Capacity = r.Capacity,
                ImageName = r.ImageName,
                RoomPrice = r.RoomPrice,
                LastReserveTime = r.reserveDates.SingleOrDefault(d => d.ReserveTime >= DateTime.Now && !d.IsReserve).ReserveTime,
                AdvantagesRoom = r.advantageToRooms.Select(a => a.advantageRoom).ToList()

            }).ToList();

            return new ShowAllRoomsViewModel() { Id = id, showSingleRooms = showRoom };
        }

        public void InsertRoomToHotel(HotelRoom room)
        {
            _context.hotelRooms.Add(room);
        }

        public HotelRoom GetRoomById(int id)
        {
            return _context.hotelRooms.Include(d => d.reserveDates).SingleOrDefault(r => r.Id == id) ?? throw new Exception();
        }

        public UpdateAndRemoveRoomVm GetHotelRoom(int id)
        {
            var room = GetRoomById(id);
            return new UpdateAndRemoveRoomVm()
            {
                Id = room.Id,
                BedCount = room.BedCount,
                Capacity = room.Capacity,
                Count = room.Count,
                Description = room.Description,
                ImageName = room.ImageName,
                RoomPrice = room.RoomPrice,
                Title = room.Title,
                IsActive = room.IsActive,
                HotelId = room.HotelId
            };
        }

        public int UpdateHotelRoom(UpdateAndRemoveRoomVm vm)
        {
            var room = GetRoomById(vm.Id);
            if (room != null)
            {
                try
                {
                    if (vm.File != null)
                    {
                        string lastImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/asset/img/RoomHotel", vm.ImageName);
                        File.Delete(lastImage);

                        string imgName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(vm.File.FileName);
                        string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/asset/img/RoomHotel", imgName);

                        using (var strem = new FileStream(imgPath, FileMode.Create))
                        {
                            vm.File.CopyTo(strem);
                        }

                        room.ImageName = imgName;
                    }

                    if (vm.IsActive && !room.reserveDates.Any(r => r.ReserveTime >= DateTime.Now))
                    {
                        return 2;
                    }

                    room.BedCount = vm.BedCount;
                    room.IsActive = vm.IsActive;
                    room.RoomPrice = vm.RoomPrice;
                    room.Title = vm.Title;
                    room.Capacity = vm.Capacity;
                    room.Description = vm.Description;
                    room.Count = vm.Count;

                    _context.hotelRooms.Update(room);
                    SaveChange();
                    return 3;

                }
                catch (Exception)
                {
                    return 1;
                }
            }
            return 0;
        }


        public int DeleteHotelRoom(int id)
        {
            var room = GetRoomById(id);
            if (room != null)
            {
                try
                {
                    string lastImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/asset/img/RoomHotel", room.ImageName);
                    File.Delete(lastImage);

                    _context.hotelRooms.Remove(room);
                    SaveChange();
                    return 1;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            return 2;
        }
        #endregion

        #region Advantage
        public ICollection<AdvantageRoom> ShowAllAdvantage()
        {
            return _context.advantageRooms.ToList();
        }

        public AdvantageRoom FindAdvantageRoom(int id)
        {
            return _context.advantageRooms.SingleOrDefault(x => x.Id == id);
        }

        public void CreateAdvantage(AdvantageRoom incomeModel)
        {
            _context.advantageRooms.Add(incomeModel);
        }

        public void UpdateAdvantage(AdvantageRoom incomeModel)
        {
            _context.advantageRooms.Update(incomeModel);
        }

        public void RemoveAdvantage(AdvantageRoom incomeModel)
        {
            _context.advantageRooms.Remove(incomeModel);
        }

        #endregion

        #region AdvatnageToRoom 
        public ShowAdvantagesRoomsVm ShowAdvantagesRoomVm(int id)
        {
            var room = _context.hotelRooms.SingleOrDefault(x => x.Id == id);
            return new ShowAdvantagesRoomsVm()
            {
                RoomId = room.Id,
                Name = room.Title,
                advantagesRoom = _context.advantageToRs.Include(a => a.advantageRoom)
                .Where(x => x.RoomId == id).Select(x => x.advantageRoom).ToList(),
            };
        }

        public InsertAdvantagesToRoomVm GetAdvantagesForInsert(int id)
        {
            var room = _context.hotelRooms.SingleOrDefault(x => x.Id == id);
            if (room != null)
            {
                return new InsertAdvantagesToRoomVm()
                {
                    advantagesRoom = _context.advantageRooms.ToList(),
                    RoomId = room.Id,
                    AdvantagesId = _context.advantageToRs
                    .Where(x => x.RoomId == room.Id).Select(c => c.AdvantageId).ToList()
                };
            }

            return null;
        }

        public void InsertAdvantageToRoom(AdvantageToRoom IncomeModel)
        {
            _context.advantageToRs.Add(IncomeModel);
        }

        public void InsertAdvantageToRoom(List<AdvantageToRoom> IncomeModel)
        {
            _context.advantageToRs.AddRange(IncomeModel);
        }

        public void RemoveAdvantagesForRoom(int id)
        {
            var advantage = _context.advantageToRs.Where(x => x.RoomId == id);
            _context.advantageToRs.RemoveRange(advantage);
            _context.SaveChanges();

        }

        #endregion

        #region ReserveDate

        public ShowAllReserveDateVm ShowAllReserveDate(int id)
        {
            return new ShowAllReserveDateVm()
            {
                reserveDates = _context.reserveDates.Where(x => x.RoomId == id && x.ReserveTime.Date >= DateTime.Now.Date).ToList(),
                Id = id
            };
        }

        public InsertReserveDateVm GetInsertRDtoRoom(int id)
        {
            var dateTimes = new List<DateTime>();

            var EndDate = DateTime.Now.AddMonths(1);
            var Duration = Convert.ToInt32((EndDate - DateTime.Now).TotalDays);

            for (int i = 0; i < Duration; i++)
            {
                dateTimes.Add(DateTime.Now.AddDays(i));
            }


            return new InsertReserveDateVm()
            {
                roomId = id,
                reserveDates = dateTimes
            };
        }

        private bool IsDateExist(DateTime date , int Id)
        {
            return _context.reserveDates.Any(x => x.ReserveTime.Date == date.Date && x.RoomId == Id);
        }

        public int InsertRDtoRoom(InsertReserveDateVm viewModel)  
        {
            try
            {
                var room = GetRoomById(viewModel.roomId);
                var dates = new List<ReserveDate>();
                foreach (var item in viewModel.reserveDates)
                {
                    if (!IsDateExist(item , room.Id))
                    {
                        dates.Add(new ReserveDate()
                        {
                            RoomId = room.Id,
                            Count = room.Count,
                            ReserveTime = item,
                            Price = room.RoomPrice
                        });
                    }
                }
                _context.reserveDates.AddRange(dates);
                SaveChange();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public UpdateReserveDateVm GetReserveDateForUpdate(int id)
        {
            var date = _context.reserveDates.SingleOrDefault(x => x.Id == id);
            if(date != null)
            {
                return new UpdateReserveDateVm()
                {
                    Id = date.Id,
                    Count = date.Count,
                    IsReserve = date.IsReserve,
                    Price = date.Price,
                    ReserveTime = PersianDate.ToShamsiDate(date.ReserveTime)
                };
            }
            return null;
        }

        public ReserveDate GetReserveDate(int id)
        {
            return _context.reserveDates.SingleOrDefault(x => x.Id == id);
        }

        public void UpdateReserveDate(ReserveDate incomeModel)
        {
            _context.reserveDates.Update(incomeModel);
        }

        public void RemoveReserveDate(ReserveDate incomeModel)
        {
            _context.reserveDates.Remove(incomeModel);
        }
        #endregion

        public void SaveChange()
        {
            _context.SaveChanges();
        }

    }
}
