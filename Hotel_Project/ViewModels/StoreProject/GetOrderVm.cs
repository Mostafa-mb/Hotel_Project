namespace Hotel_Project.ViewModels.StoreProject
{
    public class GetOrderVm
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public List<int> Dates { get; set; }
    }
}
