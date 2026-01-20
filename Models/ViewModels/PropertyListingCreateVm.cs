using System.ComponentModel.DataAnnotations;

namespace RealEstateWeb.Models.ViewModels
{
    public class PropertyListingCreateVm
    {
        // PROPERTY
        [Required, MaxLength(200)]
        public string AddressLine { get; set; } = null!;

        [Required, MaxLength(100)]
        public string City { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Region { get; set; } = null!;

        [Required]
        public int PropertyTypeId { get; set; }

        public int? PropertySize { get; set; }
        public int? NumBedrooms { get; set; }
        public int? NumBathrooms { get; set; }
        public int? NumCarspaces { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        // LISTING
        [Required]
        public int ListingTypeId { get; set; }

        [Required]
        public int ListingStatusId { get; set; }

        [Required]
        public int Price { get; set; }
    }
}
