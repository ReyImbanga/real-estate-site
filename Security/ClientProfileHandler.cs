using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RealEstateWeb.Data;

namespace RealEstateWeb.Security
{
    public class ClientProfileHandler : AuthorizationHandler<ClientProfileRequirement>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientProfileHandler(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientProfileRequirement requirement)
        {
            if (!context.User.Identity?.IsAuthenticated ?? true)
                return;

            var user = await _userManager.GetUserAsync(context.User);
            if (user == null)
                return;

            var hasClientProfile = _context.ClientProfiles
                .Any(c => c.ApplicationUserId == user.Id);

            if (hasClientProfile)
            {
                context.Succeed(requirement);
                
            }
        }
    }
}
