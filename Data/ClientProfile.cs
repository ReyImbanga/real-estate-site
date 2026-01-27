using System.ComponentModel.DataAnnotations;

namespace RealEstateWeb.Data
{
    public class ClientProfile
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        /*[Required, MaxLength(320)]
        public string EmailAddress { get; set; } = null!;*/

        //[Required, MaxLength(30)]
        //public string PhoneNumber { get; set; } = null!;
        public DateOnly? DateOfBirth { get; set; }

        // FK -> AspNetUsers
        [Required]
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
