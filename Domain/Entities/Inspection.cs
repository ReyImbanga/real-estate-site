using RealEstateWeb.Data;

namespace RealEstateWeb.Domain.Entities
{
    public class Inspection
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; } = null!;

        public int ResponsibleOwnerId { get; set; }
        public OwnerProfile ResponsibleOwner { get; set; } = null!;

        public DateTime InspectionDateTime { get; set; }
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ClientInspection> ClientInspections { get; set; }
            = new List<ClientInspection>();
    }
}
