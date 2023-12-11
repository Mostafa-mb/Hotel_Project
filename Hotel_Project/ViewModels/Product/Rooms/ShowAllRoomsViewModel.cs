namespace Hotel_Project.ViewModels.Product.Rooms
{
    public class ShowAllRoomsViewModel
    {
        public int Id { get; set; }
        public IEnumerable<ShowSingleRoomVm> showSingleRooms { get; set; }
    }
}
