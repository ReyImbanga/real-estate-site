using RealEstateWeb.Data;

namespace RealEstateWeb.Domain.Entities
{
    public class Contract
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; } = null!;

        public int ListingId { get; set; }
        public Listing Listing { get; set; } = null!;

        public int ClientProfileId { get; set; }
        public ClientProfile ClientProfile { get; set; } = null!;

        public int ResponsibleOwnerId { get; set; }
        public OwnerProfile ResponsibleOwner { get; set; } = null!;

        public int ContractStatusId { get; set; }
        public ContractStatus ContractStatus { get; set; } = null!;

        public DateOnly SignedDate { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public string? ContractDocumentPath { get; set; }
    }
}
