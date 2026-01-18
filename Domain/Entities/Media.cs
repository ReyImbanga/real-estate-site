using System.ComponentModel.DataAnnotations;

namespace RealEstateWeb.Domain.Entities
{
    public class Media
    {
        public int Id { get; set; }

        [Required, MaxLength(500)]
        public string MediaPath { get; set; } = null!;

        // FK → MediaType
        [Required]
        public int MediaTypeId { get; set; }
        public MediaType MediaType { get; set; } = null!;

        // FK → Property
        [Required]
        public int PropertyId { get; set; }
        public Property Property { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
