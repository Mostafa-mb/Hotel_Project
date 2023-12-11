using System.ComponentModel.DataAnnotations;
namespace Hotel_Project.ViewModels.Product.RoomReserveDate
{
    public class UpdateReserveDateVm
    {
        public int Id { get; set; }

        [Display(Name = "تاریخ رزرو")]
        public string? ReserveTime { get; set; }

        [Display(Name = "تعداد اتاق")]
        public int Count { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Display(Name = "وضعیت رزرو")]
        public bool IsReserve { get; set; }
    }
}
