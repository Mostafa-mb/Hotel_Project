using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Project.Models.Product
{
    public class AdvantageToRoom
    {
        public int RoomId { get; set; }
        public int AdvantageId { get; set; }

        [ForeignKey(nameof(RoomId))]
        public HotelRoom hotelRoom { get; set; }
        [ForeignKey(nameof(AdvantageId))]
        public AdvantageRoom advantageRoom { get; set; }
    }
}
