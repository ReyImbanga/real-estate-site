using System.ComponentModel.DataAnnotations;

namespace RealEstateWeb.Domain.Entities
{
    public class MediaType
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Description { get; set; } = null!;

        public ICollection<Media> Medias { get; set; }
            = new List<Media>();
    }
}
