using System.ComponentModel.DataAnnotations;

namespace RealEstateWeb.Domain.Entities
{
    public class Listing
    {
        public int Id { get; set; }

        // FK → Property
        [Required]
        public int PropertyId { get; set; }
        public Property Property { get; set; } = null!;

        // FK → ListingType (Location / Vente)
        [Required]
        public int ListingTypeId { get; set; }
        public ListingType ListingType { get; set; } = null!;

        // FK → ListingStatus (Actif / Vendu / Loué)
        [Required]
        public int ListingStatusId { get; set; }
        public ListingStatus ListingStatus { get; set; } = null!;

        [Required]
        public int Price { get; set; }

        [Required]
        public DateOnly CreatedDate { get; set; }

        public DateTime? DeletedAt { get; set; }

        // Navigation future
        public ICollection<ListingPriceHistory> PriceHistories { get; set; }
            = new List<ListingPriceHistory>();
    }
}
