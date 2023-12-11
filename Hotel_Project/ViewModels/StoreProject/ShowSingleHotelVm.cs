using Hotel_Project.Models.Product;

namespace Hotel_Project.ViewModels.StoreProject
{
    public class ShowSingleHotelVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RoomCount { get; set; }
        public int StageCount { get; set; }
        public string EntryTime { get; set; }
        public string ExitTime { get; set; }


        public ICollection<HotelGallery> hotelGalleries { get; set; }
        public ICollection<HotelRule> hotelRules { get; set; }
        public ICollection<RoomListVm> hotelRooms { get; set; }

    }
}
