using RealEstateWeb.Data;

namespace RealEstateWeb.Domain.Entities
{
    public class ClientInspection
    {
        public int ClientProfileId { get; set; }
        public ClientProfile ClientProfile { get; set; } = null!;

        public int InspectionId { get; set; }
        public Inspection Inspection { get; set; } = null!;
    }
}
