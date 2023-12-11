using Hotel_Project.Models.Product;

namespace Hotel_Project.ViewModels.StoreProject
{
    public class BasketDetailViewModel
    {
        public string HotelName { get; set; }
        public int DetailId { get; set; }
        public string RoomName { get; set; }
        public int BasePrice { get; set; }
        public long TotalPrice { get; set; }
        public List<ReserveDate> reserveDates { get; set; }
    }
}
