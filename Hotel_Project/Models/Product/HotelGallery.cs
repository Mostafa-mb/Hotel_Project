using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Project.Models.Product
{
    public class HotelGallery
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="تصویر")]
        [Required(ErrorMessage ="لطفا {0} را کامل کنید")] 
        public string ImageName { get; set; }


        #region Navigation

        public int HotelId { get; set; }

        [ForeignKey(nameof(HotelId))]
        public Hotel hotel { get; set; }

        #endregion
    }
}
