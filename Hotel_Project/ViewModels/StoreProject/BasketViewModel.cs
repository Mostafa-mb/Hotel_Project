namespace Hotel_Project.ViewModels.StoreProject
{
    public class BasketViewModel
    {
        public long OrderSum { get; set; }
        public List<BasketDetailViewModel> basketDetails { get; set; }
    }
}
