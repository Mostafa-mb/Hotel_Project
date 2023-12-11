using Hotel_Project.Models.Product;

namespace Hotel_Project.ViewModels.Product.Advantage
{
    public class ShowAdvantagesRoomsVm
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
        public List<AdvantageRoom> advantagesRoom { get; set; }
    }
}
