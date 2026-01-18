using System.ComponentModel.DataAnnotations;

namespace RealEstateWeb.Domain.Entities
{
    public class ListingPriceHistory
    {
        public int Id { get; set; }

        // FK → Listing
        [Required]
        public int ListingId { get; set; }
        public Listing Listing { get; set; } = null!;

        [Required]
        public int Price { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }
    }
}
