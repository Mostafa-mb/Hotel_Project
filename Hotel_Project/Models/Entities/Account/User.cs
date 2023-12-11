using Hotel_Project.Models.Basket;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Project.Models.Entities.Account
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="ایمیل")]
        [Required(ErrorMessage ="لطفا {0} را کامل کنید")] 
        public string Email { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        public string Password { get; set; }

        [Display(Name = "نام")]
        public string? Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string? LastName { get; set; }


        public ICollection<Order> Orders { get; set; }
    }
}
