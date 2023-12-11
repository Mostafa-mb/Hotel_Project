using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Hotel_Project.ViewModels.Product.HotelRulesVm
{
    public class CrudForHotelRuleVm
    {
        public int Id { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را کامل کنید")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر ها نمیتواند بیشتر از {1} باشد")]
        [MinLength(2, ErrorMessage = "تعداد کاراکتر ها نمیتواند کمتر از {1} باشد")]
        public string Description { get; set; }
    }
}
