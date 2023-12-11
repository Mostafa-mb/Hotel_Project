using Hotel_Project.Models.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Project.Models.Basket
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int orderId { get; set; }
        public int Price { get; set; }
        public int RoomId { get; set; }

        [ForeignKey(nameof(RoomId))]
        public HotelRoom hotelRoom { get; set; }

        public ICollection<OrderReserveDate> orderReserveDates { get; set; }
    }
}
