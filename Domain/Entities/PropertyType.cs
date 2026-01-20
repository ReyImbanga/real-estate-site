using System.ComponentModel.DataAnnotations;

namespace RealEstateWeb.Domain.Entities
{
    public class PropertyType
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string? Description { get; set; } 

        // Navigation
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
