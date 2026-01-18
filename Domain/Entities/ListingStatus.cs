using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RealEstateWeb.Domain.Entities
{
    public class ListingStatus
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Description { get; set; } = null!;

        public ICollection<Listing> Listings { get; set; } = new List<Listing>();
    }
}
