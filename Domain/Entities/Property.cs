using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RealEstateWeb.Domain.Entities
{
    public class Property
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string AddressLine { get; set; } = null!;

        [Required, MaxLength(100)]
        public string City { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Region { get; set; } = null!;

        // FK → PropertyType
        [Required]
        public int? PropertyTypeId { get; set; }
        public PropertyType? PropertyType { get; set; }

        public int? PropertySize { get; set; }

        public int? NumBedrooms { get; set; }

        public int? NumBathrooms { get; set; }

        public int? NumCarspaces { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        /* =======================
           NAVIGATIONS (préparées)
           ======================= */

        public ICollection<Listing> Listings { get; set; } = new List<Listing>();
        public ICollection<PropertyFeature> PropertyFeatures { get; set; } = new List<PropertyFeature>();
        public ICollection<Media> Medias { get; set; } = new List<Media>();
        //public ICollection<Ownership> Ownerships { get; set; } = new List<Ownership>();
        //public ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();
    }
}
