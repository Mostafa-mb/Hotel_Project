using Hotel_Project.Models.Product;
using System.ComponentModel.DataAnnotations;


namespace Hotel_Project.ViewModels.Product.Rooms
{
    public class ShowSingleRoomVm
    {
        public int Id { get; set; }

        [Display(Name = "تصویر اتاق")]
        public string ImageName { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "قیمت")]
        public int RoomPrice { get; set; }
        [Display(Name = "ظرفیت")]
        public int Capacity { get; set; }
        [Display(Name = "تعداد تخت")]
        public int BedCount { get; set; }

        public DateTime? LastReserveTime { get; set; }
        public ICollection<AdvantageRoom> AdvantagesRoom { get; set; }
    }
}
