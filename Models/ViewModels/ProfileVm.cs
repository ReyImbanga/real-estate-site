using System.ComponentModel.DataAnnotations;

namespace RealEstateWeb.Models.ViewModels
{
    public class ProfileVm
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = null!;

        [Required]
        public DateOnly DateOfBirth { get; set; }
    }
}
