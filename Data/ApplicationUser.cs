using Microsoft.AspNetCore.Identity;

namespace RealEstateWeb.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Navigation 1–1 (optionnel mais recommandé)
        public ClientProfile? ClientProfile { get; set; }
        public OwnerProfile? OwnerProfile { get; set; }
    }
}
