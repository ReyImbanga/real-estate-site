using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Data;

namespace RealEstateWeb.Pages.Dashboard
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public bool IsProfileCompleted { get; set; }

        // Message flash
        public string? StatusMessage { get; set; }


        public async Task OnGetAsync(string? statusMessage = null)
        {
            StatusMessage = statusMessage;

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return;

            var client = await _context.ClientProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

            IsProfileCompleted =
                client != null &&
                !string.IsNullOrWhiteSpace(client.FirstName) &&
                !string.IsNullOrWhiteSpace(client.LastName) &&
                client.DateOfBirth != null;
        }
    }
}
