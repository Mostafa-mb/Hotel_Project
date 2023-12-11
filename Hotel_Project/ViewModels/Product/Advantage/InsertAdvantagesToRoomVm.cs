using Hotel_Project.Models.Product;

namespace Hotel_Project.ViewModels.Product.Advantage
{
    public class InsertAdvantagesToRoomVm
    {
        public int RoomId { get; set; }
        public List<int> AdvantagesId { get; set; }
        public List<AdvantageRoom>? advantagesRoom { get; set; }
         
    }
}
