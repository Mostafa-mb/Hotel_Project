using System.ComponentModel.DataAnnotations;

namespace Hotel_Project.Models.Entities.Web
{
    public class FisrtBaner
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage ="لطفا {0} را کامل کنید")] 
        public string BanerTitle { get; set; }

        [Display(Name = "متن دکمه")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public string BanerButton { get; set; }

        [Display(Name = "تصویر بنر")]
        public string ImageName { get; set; }


    }
}
