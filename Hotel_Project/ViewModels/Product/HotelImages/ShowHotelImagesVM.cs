using Hotel_Project.Models.Product;

namespace Hotel_Project.ViewModels.Product.HotelImages
{
    public class ShowHotelImagesVM
    {
        public int Id { get; set; }
        public IEnumerable<HotelGallery> HotelGalleries { get; set; }
    }
}
