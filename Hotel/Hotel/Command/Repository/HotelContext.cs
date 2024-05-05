using Hotel.Command.Model.Event;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Command.Repository
{
    public class HotelContext : DbContext
    {
        public DbSet<ActiveBookedReservationEvent> ActiveReservations { get; set; }
        public DbSet<BookedReservationEvent> BookedReservations { get; set; }
        public DbSet<BookedHotelRoomsEvent> BookedHotelRooms { get; set; }
        public DbSet<CanceledReservationEvent> CanceledReservations { get; set; }

        public DbSet<CreatedHotelRoomTypeEvent> HotelRoomTypes { get; set; }

        private readonly IConfiguration configuration;

        public HotelContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(configuration.GetConnectionString("DbPath"));
    }
}
