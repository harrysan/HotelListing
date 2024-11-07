using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Indonesia",
                    ShortName = "ID",
                },
                new Country
                {
                    Id = 2,
                    Name = "Bahamas",
                    ShortName = "BS",
                },
                new Country
                {
                    Id = 3,
                    Name = "Japan",
                    ShortName = "JP",
                });

                modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Mercure Hotel Bali",
                    Address = "Jalan Ubud",
                    CountryId = 1,
                    Rating = 4.5
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Holiday Inn Hotel Bahamas",
                    Address = "Bahamas Street",
                    CountryId = 2,
                    Rating = 4.3
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Tavinos Hotel Japan",
                    Address = "Japan Tokyo City",
                    CountryId = 3,
                    Rating = 4.7
                });
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
