using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
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
            }
            );
        }
    }
}
