using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Hotel_Project.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "تکرار کلمه عبور همخوانی ندارد")]
        public string RePassword { get; set; }

    }
}
