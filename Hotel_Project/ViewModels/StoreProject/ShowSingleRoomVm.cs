using Hotel_Project.Models.Product;

namespace Hotel_Project.ViewModels.StoreProject
{
    public class ShowSingleRoomVm
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Title { get; set; }
        public int BedCount { get; set; }
        public int Capacity { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ICollection<ReserveDate> reserveDates { get; set; }
        public ICollection<AdvantageRoom> advantageRooms { get; set; }

    }
}
