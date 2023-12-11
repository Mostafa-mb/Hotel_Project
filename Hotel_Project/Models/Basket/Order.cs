using Hotel_Project.Models.Entities.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Project.Models.Basket
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public long OrderSum { get; set; }
        public int HotelId { get; set; }
        public int PassCode { get; set; }
        public int Count { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Description { get; set; }
        public bool IsFinally { get; set; }
        public DateTime CreateDate { get; set; }


        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public ICollection<OrderDetail> orderDetails { get; set; }
    }
}
