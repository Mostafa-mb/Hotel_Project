namespace Hotel_Project.ViewModels.StoreProject
{
    public class CheckoutViewModel
    {
        public long OrderSum { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int PassCode { get; set; }
        public int Count { get; set; }
        public List<BasketDetailViewModel> basketDetails { get; set; }
    }
}
