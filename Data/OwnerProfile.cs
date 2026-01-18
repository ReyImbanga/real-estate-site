using System.ComponentModel.DataAnnotations;

namespace RealEstateWeb.Data
{
    public class OwnerProfile
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = null!;

        // Tu peux garder StartDate comme dans ton schéma
        public DateOnly? StartDate { get; set; }

        // FK -> AspNetUsers
        [Required]
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
