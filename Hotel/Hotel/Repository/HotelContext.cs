using Hotel.Model.Event;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Repository
{
    public class HotelContext : DbContext
    {
        public DbSet<ActiveBookedReservationEvent> ActiveReservations { get; set; }
        public DbSet<BookedReservationEvent> BookedReservations { get; set; }
        public DbSet<BookedHotelRoomsEvent> BookedHotelRooms { get; set; }
        public DbSet<CanceledReservationEvent> CanceledReservations { get; set;}

        public DbSet<CreatedHotelRoomType> HotelRoomTypes { get; set; }

        private readonly IConfiguration configuration;

        public HotelContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(configuration.GetConnectionString("DbPath"));
    }
}
