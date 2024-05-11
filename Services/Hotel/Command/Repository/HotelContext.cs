using Hotel.Command.Model;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Command.Repository
{
    public class HotelContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<BookedReservationEvent> BookedReservations { get; set; }
        public DbSet<BookedHotelRoomsEvent> BookedHotelRooms { get; set; }
        public DbSet<CanceledReservationEvent> CanceledReservations { get; set; }

        public DbSet<HotelRoomType> HotelRoomTypes { get; set; }

        private readonly IConfiguration configuration;

        public HotelContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(configuration.GetConnectionString("DbPath"));
    }
}
