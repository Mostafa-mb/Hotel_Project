using System.ComponentModel.DataAnnotations;

namespace Hotel_Project.ViewModels.Product.Hotel
{
    public class EditHotelViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [MaxLength(50, ErrorMessage = "تعداد کاراکتر ها نمیتواند بیشتر از {1} باشد")]
        [MinLength(2, ErrorMessage = "تعداد کاراکتر ها نمیتواند کمتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر ها نمیتواند بیشتر از {1} باشد")]
        [MinLength(20, ErrorMessage = "تعداد کاراکتر ها نمیتواند کمتر از {1} باشد")]
        public string Description { get; set; }

        [Display(Name = "تعداد اتاق")]
        public int RoomCount { get; set; }
        [Display(Name = "تعداد طبقه")]
        public int StageCount { get; set; }

        [Display(Name = "زمان ورود")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public string EntryTime { get; set; }

        [Display(Name = "زمان خروج")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public string ExitTime { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        #region Address

        [Display(Name = "آدرس هتل")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر ها نمیتواند بیشتر از {1} باشد")]
        [MinLength(10, ErrorMessage = "تعداد کاراکتر ها نمیتواند کمتر از {1} باشد")]
        public string Address { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [MaxLength(35, ErrorMessage = "تعداد کاراکتر ها نمیتواند بیشتر از {1} باشد")]
        [MinLength(2, ErrorMessage = "تعداد کاراکتر ها نمیتواند کمتر از {1} باشد")]
        public string City { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [MaxLength(35, ErrorMessage = "تعداد کاراکتر ها نمیتواند بیشتر از {1} باشد")]
        [MinLength(2, ErrorMessage = "تعداد کاراکتر ها نمیتواند کمتر از {1} باشد")]
        public string State { get; set; }

        [Display(Name = "کد پستی")]
        [MaxLength(10, ErrorMessage = "تعداد کاراکتر ها نمیتواند بیشتر از {1} باشد")]
        [MinLength(10, ErrorMessage = "تعداد کاراکتر ها نمیتواند کمتر از {1} باشد")]
        public string? PostalCode { get; set; }

        #endregion
    }
}
