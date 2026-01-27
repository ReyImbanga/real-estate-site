using RealEstateWeb.Data;
using System.ComponentModel.DataAnnotations;

namespace RealEstateWeb.Domain.Entities
{
    public class Ownership
    {
        public int PropertyId { get; set; }
        public Property Property { get; set; } = null!;

        public int OwnerProfileId { get; set; }
        public OwnerProfile OwnerProfile { get; set; } = null!;

        public int RoleTypeId { get; set; }
        public RoleType RoleType { get; set; } = null!;

        [Range(0, 100)]
        public decimal SharePercent { get; set; }

        public bool IsPrimary { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
