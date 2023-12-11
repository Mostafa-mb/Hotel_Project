using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Project.Models.Product
{
    public class ReserveDate
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="تاریخ رزرو")]
        [Required(ErrorMessage ="لطفا {0} را کامل کنید")] 
        public DateTime ReserveTime { get; set; }

        [Display(Name ="تعداد اتاق")]
        public int Count { get; set; }

        [Display(Name ="قیمت")]
        public int Price { get; set; }
        [Display(Name ="وضعیت رزرو")]
        public bool IsReserve { get; set; }

        public int RoomId { get; set; }

        [ForeignKey(nameof(RoomId))]
        public HotelRoom HotelRoom { get; set; }
    }
}
