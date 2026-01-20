using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using RealEstateWeb.Data;
using System.Security.Claims;

namespace RealEstateWeb.Security
{
    public class OwnerClaimsTransformation : IClaimsTransformation
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OwnerClaimsTransformation(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (!principal.Identity?.IsAuthenticated ?? true)
                return principal;

            var user = await _userManager.GetUserAsync(principal);
            if (user == null) return principal;

            var identity = (ClaimsIdentity)principal.Identity;

            if (!identity.HasClaim(c => c.Type == "OwnerProfile"))
            {
                var hasOwnerProfile = _context.OwnerProfiles
                    .Any(o => o.ApplicationUserId == user.Id);

                if (hasOwnerProfile)
                {
                    identity.AddClaim(new Claim("OwnerProfile", "true"));
                }
            }

            return principal;
        }
    }
}
