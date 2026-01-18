using System.ComponentModel.DataAnnotations;

namespace RealEstateWeb.Domain.Entities
{
    public class Feature
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FeatureName { get; set; } = null!;

        public ICollection<PropertyFeature> PropertyFeatures { get; set; }
            = new List<PropertyFeature>();
    }
}
