using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Project.Models.Product
{
    public class HotelRule
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="توضیحات")]
        [Required(ErrorMessage ="لطفا {0} را کامل کنید")] 
        [MaxLength(500 , ErrorMessage ="تعداد کاراکتر ها نمیتواند بیشتر از {1} باشد")]
        [MinLength(2 , ErrorMessage = "تعداد کاراکتر ها نمیتواند کمتر از {1} باشد")]
        public string Description { get; set; }

        #region Navigation

        public int HotelId { get; set; }

        [ForeignKey(nameof(HotelId))]
        public Hotel hotel { get; set; }

        #endregion
    }
}
