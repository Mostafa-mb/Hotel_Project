using Hotel_Project.Models.Basket;
using Hotel_Project.Models.Entities.Account;
using Hotel_Project.Models.Entities.Web;
using Hotel_Project.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Project.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdvantageToRoom>().HasKey(ar => new { ar.RoomId , ar.AdvantageId });

            base.OnModelCreating(modelBuilder);
        }

        #region Web
        public DbSet<FisrtBaner> fisrtBaners { get; set; }

        public DbSet<User> users { get; set; }
        #endregion

        #region Product

        public DbSet<AdvantageRoom> advantageRooms { get; set; }
        public DbSet<AdvantageToRoom> advantageToRs { get; set; }
        public DbSet<Hotel> hotels { get; set; }
        public DbSet<HotelAddress> hotelAddresses { get; set; }
        public DbSet<HotelGallery> hotelGalleries { get; set; }
        public DbSet<HotelRoom> hotelRooms { get; set; }
        public DbSet<HotelRule> hotelRules { get; set; }
        public DbSet<ReserveDate> reserveDates { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderReserveDate> OrderReserveDates { get; set; }

        #endregion
    }
}
