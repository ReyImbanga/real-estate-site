using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstateWeb.Data;

namespace RealEstateWeb.Pages.Owners
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext context,  UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public OwnerProfile OwnerProfile { get; set; } = new();
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var alreadyOwner = _context.OwnerProfiles
                .Any(o => o.ApplicationUserId == user!.Id);

            if (alreadyOwner)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var alreadyOwner = _context.OwnerProfiles
                .Any(o => o.ApplicationUserId == user!.Id);

            if (alreadyOwner)
            {
                return RedirectToPage("/Index");
            }

            OwnerProfile.ApplicationUserId = user!.Id;
            OwnerProfile.StartDate = DateOnly.FromDateTime(DateTime.Today);

            _context.OwnerProfiles.Add(OwnerProfile);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Properties/Index");
        }
    }
}
