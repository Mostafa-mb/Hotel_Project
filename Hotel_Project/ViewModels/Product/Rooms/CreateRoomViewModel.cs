using System.ComponentModel.DataAnnotations;

namespace Hotel_Project.ViewModels.Product.Rooms
{
    public class CreateRoomViewModel
    {
        public int HotelId { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public IFormFile File { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر ها نمیتواند بیشتر از {1} باشد")]
        [MinLength(2, ErrorMessage = "تعداد کاراکتر ها نمیتواند کمتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر ها نمیتواند بیشتر از {1} باشد")]
        [MinLength(2, ErrorMessage = "تعداد کاراکتر ها نمیتواند کمتر از {1} باشد")]
        public string Description { get; set; }

        [Display(Name = "قیمت")]
        public int RoomPrice { get; set; }
        [Display(Name = "تعداد")]
        public int Count { get; set; }
        [Display(Name = "ظرفیت")]
        public int Capacity { get; set; }
        [Display(Name = "تعداد تخت")]
        public int BedCount { get; set; }
    }
}
