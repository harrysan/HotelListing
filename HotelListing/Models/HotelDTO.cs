using HotelListing.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Models
{
    public class CreateHotelDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Hotel Name is Too Long")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(maximumLength:250, ErrorMessage = "Hotel Address is Too Long")]
        public string Address { get; set; } = string.Empty;
        [Required]
        [Range(1, 5)]
        public double Rating { get; set; }
        [Required]
        public int CountryId { get; set; }
    }

    public class HotelDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; }
    }
}
