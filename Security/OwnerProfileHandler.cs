using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RealEstateWeb.Data;

namespace RealEstateWeb.Security
{
    public class OwnerProfileHandler : AuthorizationHandler<OwnerProfileRequirement>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public OwnerProfileHandler(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerProfileRequirement requirement)
        {
            if (!context.User.Identity?.IsAuthenticated ?? true)
                return;

            var user = await _userManager.GetUserAsync(context.User);
            if (user == null) return;

            var hasOwnerProfile = _context.OwnerProfiles.Any(o => o.ApplicationUserId == user.Id);

            if (hasOwnerProfile)
            {
                context.Succeed(requirement);
            }
        }
    }
}
