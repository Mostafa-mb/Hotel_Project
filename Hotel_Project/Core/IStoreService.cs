using Hotel_Project.ViewModels.Account;
using Hotel_Project.ViewModels.StoreProject;

namespace Hotel_Project.Core
{
    public interface IStoreService
    {
        List<ShowStoreHotels> ShowStore();
        ShowSingleHotelVm ShowSingleHotel(long id);
        ShowSingleRoomVm ShowSingleRoom(long id);
        int CreateOrder(GetOrderVm incomeModel);
        BasketViewModel GetUserBasket(int userId);
        CheckoutViewModel GetUserCheckout(int userId);
        int PaymentOrder(int userId, CheckoutViewModel incomeModel);
        int RemoveDetail(int id);
        ICollection<UserOrdersViewModel> UserOrders(int userId);
    }
}
