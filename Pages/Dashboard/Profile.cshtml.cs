using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Data;
using RealEstateWeb.Models.ViewModels;

namespace RealEstateWeb.Pages.Dashboard
{
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public ProfileVm Input { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var client = await _context.ClientProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

            if (client != null)
            {
                Input.FirstName = client.FirstName ?? string.Empty;
                Input.LastName = client.LastName ?? string.Empty;

                if (client.DateOfBirth.HasValue)
                {
                    Input.DateOfBirth = client.DateOfBirth.Value;
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var client = await _context.ClientProfiles
                .FirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

            if (client == null)
            {
                // Cr?ation du ClientProfile
                client = new ClientProfile
                {
                    ApplicationUserId = user.Id,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    DateOfBirth = Input.DateOfBirth
                };

                _context.ClientProfiles.Add(client);
            }

            else
            {
                // Mise ? jour
                client.FirstName = Input.FirstName;
                client.LastName = Input.LastName;
                client.DateOfBirth = Input.DateOfBirth;
            }

            await _context.SaveChangesAsync();

            //return RedirectToPage("/Dashboard/Index");
            if (!string.IsNullOrWhiteSpace(ReturnUrl))
            {
                return LocalRedirect(ReturnUrl);
            }

            //return RedirectToPage("/Dashboard/Index");
            if (!string.IsNullOrWhiteSpace(ReturnUrl))
            {
                return LocalRedirect(ReturnUrl);
            }

            return RedirectToPage(
                "/Dashboard/Index",
                new { statusMessage = "Profil compl?t? avec succ?s ?" }
            );



        }
    }
}
