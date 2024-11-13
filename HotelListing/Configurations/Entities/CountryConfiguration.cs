﻿using HotelListing.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
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
        }
    }
}
