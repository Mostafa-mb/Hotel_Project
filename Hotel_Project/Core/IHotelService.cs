using Hotel_Project.Models.Product;
using Hotel_Project.ViewModels.Product.Advantage;
using Hotel_Project.ViewModels.Product.Hotel;
using Hotel_Project.ViewModels.Product.HotelRulesVm;
using Hotel_Project.ViewModels.Product.RoomReserveDate;
using Hotel_Project.ViewModels.Product.Rooms;

namespace Hotel_Project.Core
{
    public interface IHotelService
    {
        #region BaseHotel

        public IEnumerable<Hotel> GetAllHotels();

        public void InsertHotel(Hotel hotel);
        public void InsertAddress(HotelAddress address);

        public void UpdateHotel(Hotel hotel);
        public void UpdateAddress(HotelAddress address);
        public Hotel GetHotelById(int id);
        public EditHotelViewModel GetHotelViewModel(int id);

        public void RemoveHotel(Hotel hotel);
        public void RemoveAddress(HotelAddress address);
        #endregion

        #region HotelImages

        public IEnumerable<HotelGallery> HotelGalleries(int id);

        public void AddHotelGallery(HotelGallery hotelGallery);
        public HotelGallery GetGalleryById(int id);
        public void RemoveGallery(HotelGallery gallery);
        public bool RemoveImage(string ImageName);
        #endregion

        #region HotelRules

        public IEnumerable<HotelRule> ShowallHotelRules(int id);
        public void InsertRuleToHotel(HotelRule rule);
        public HotelRule FindHotelRule(int id);
        public CrudForHotelRuleVm GetRuleViewModel(int id);
        public void UpdateHotelRule(HotelRule rule);
        public void RemoveHotelRule(HotelRule rule);

        #endregion

        #region HotelRoom

        public ShowAllRoomsViewModel ShowAllHotelRoom(int id);
        public void InsertRoomToHotel(HotelRoom room);
        public HotelRoom GetRoomById(int id);
        public UpdateAndRemoveRoomVm GetHotelRoom(int id);
        public int UpdateHotelRoom(UpdateAndRemoveRoomVm vm);
        public int DeleteHotelRoom(int id);
        #endregion

        #region Advantage

        public ICollection<AdvantageRoom> ShowAllAdvantage();
        public AdvantageRoom FindAdvantageRoom(int id);
        public void CreateAdvantage(AdvantageRoom incomeModel);
        public void UpdateAdvantage(AdvantageRoom viewModel);
        public void RemoveAdvantage(AdvantageRoom incomeModel);

        #endregion

        #region AdvatnageToRoom 

        public ShowAdvantagesRoomsVm ShowAdvantagesRoomVm(int id);
        public InsertAdvantagesToRoomVm GetAdvantagesForInsert(int id);
        public void InsertAdvantageToRoom(AdvantageToRoom IncomeModel);
        public void InsertAdvantageToRoom(List<AdvantageToRoom> IncomeModel);
        public void RemoveAdvantagesForRoom(int id);
        #endregion

        #region ReserveDate

        ShowAllReserveDateVm ShowAllReserveDate(int id);
        InsertReserveDateVm GetInsertRDtoRoom(int id);
        int InsertRDtoRoom(InsertReserveDateVm viewModel);
        UpdateReserveDateVm GetReserveDateForUpdate(int id);
        ReserveDate GetReserveDate(int id);
        void UpdateReserveDate(ReserveDate incomeModel);
        void RemoveReserveDate(ReserveDate incomeModel);
        #endregion

        public void SaveChange();
      
    }
}
