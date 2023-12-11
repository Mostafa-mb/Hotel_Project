using Hotel_Project.Models.Product;

namespace Hotel_Project.ViewModels.StoreProject
{
    public class RoomListVm
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RoomPrice { get; set; }
        public int Count { get; set; }
        public int Capacity { get; set; }
        public int BedCount { get; set; }

        public List<AdvantageRoom> advantagesRoom { get; set; }
        public ReserveDate? LastReserveDate { get; set; }
    }
}
