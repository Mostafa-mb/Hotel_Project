using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Project.Models.Product
{
    public class HotelAddress
    {
        [Display(Name ="آدرس هتل")]
        [Required(ErrorMessage ="لطفا {0} را کامل کنید")] 
        [MaxLength(500 , ErrorMessage ="تعداد کاراکتر ها نمیتواند بیشتر از {1} باشد")]
        [MinLength(10 , ErrorMessage = "تعداد کاراکتر ها نمیتواند کمتر از {1} باشد")]
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

        #region Navigation

        [Key]
        public int HotelId { get; set; }

        [ForeignKey(nameof(HotelId))]
        public Hotel hotel { get; set; }
        #endregion
    }
}
