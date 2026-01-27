using RealEstateWeb.Data;

namespace RealEstateWeb.Domain.Entities
{
    public class Offer
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; } = null!;

        public int ClientProfileId { get; set; }
        public ClientProfile ClientProfile { get; set; } = null!;

        public int OfferStatusId { get; set; }
        public OfferStatus OfferStatus { get; set; } = null!;

        public int OfferAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
    }
}
