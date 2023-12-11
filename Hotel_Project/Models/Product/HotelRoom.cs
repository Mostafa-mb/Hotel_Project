using Hotel_Project.Models.Basket;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Project.Models.Product
{
    public class HotelRoom
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="تصویر اتاق")]
        [Required(ErrorMessage ="لطفا {0} را کامل کنید")] 
        public string ImageName { get; set; }

        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="لطفا {0} را کامل کنید")] 
        [MaxLength(100 , ErrorMessage ="تعداد کاراکتر ها نمیتواند بیشتر از {1} باشد")]
        [MinLength(2 , ErrorMessage = "تعداد کاراکتر ها نمیتواند کمتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر ها نمیتواند بیشتر از {1} باشد")]
        [MinLength(2, ErrorMessage = "تعداد کاراکتر ها نمیتواند کمتر از {1} باشد")]
        public string Description { get; set; }

        [Display(Name ="قیمت")]
        public int RoomPrice { get; set; }
        [Display(Name ="تعداد")]
        public int Count { get; set; }
        [Display(Name = "ظرفیت")]
        public int Capacity { get; set; }
        [Display(Name = "تعداد تخت")]
        public int BedCount { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        #region Navigation

        public int HotelId { get; set; }

        [ForeignKey(nameof(HotelId))]
        public Hotel hotel { get; set; }

        public ICollection<ReserveDate> reserveDates { get; set; }
        public ICollection<AdvantageToRoom> advantageToRooms { get; set; }
        public ICollection<OrderDetail> orderDetails { get; set; }
        #endregion
    }
}
