using System.ComponentModel.DataAnnotations;

namespace Hotel_Project.ViewModels.Product.HotelImages
{
    public class InsertAndRemoveImageVm
    {
        public int Id { get; set; }
        public string? ImageName { get; set; }

        [Display(Name ="تصویر")]
        [Required(ErrorMessage ="لطفا {0} را کامل کنید")] 
        public IFormFile File { get; set; }
    }
}
