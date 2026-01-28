using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Data;
using RealEstateWeb.Domain.Entities;
using RealEstateWeb.Models.ViewModels;
using RealEstateWeb.Services;

namespace RealEstateWeb.Pages.Properties
{
    //[Authorize(Policy = "RequireClientProfile")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ListingPriceService _priceService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext context, ListingPriceService priceService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _priceService = priceService;
            _userManager = userManager;
        }

        [BindProperty]
        public PropertyListingCreateVm Input { get; set; } = new();

        public SelectList PropertyTypes { get; set; } = null!;
        public SelectList ListingTypes { get; set; } = null!;
        public SelectList ListingStatuses { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var client = await _context.ClientProfiles
                .FirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

            // S?curit? (normalement impossible)
            if (client == null)
                return RedirectToPage("/Dashboard/Profile", new { returnUrl = Url.Page("/Properties/Create") });

            // PROFIL INCOMPLET REDIRECTION
            if (string.IsNullOrWhiteSpace(client.FirstName)
                || string.IsNullOrWhiteSpace(client.LastName)
                || client.DateOfBirth == null)
            {
                return RedirectToPage("/Dashboard/Profile", new { returnUrl = Url.Page("/Properties/Create") });
            }

            LoadLookups();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var client = await _context.ClientProfiles
                .FirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

            if (client == null
                || string.IsNullOrWhiteSpace(client.FirstName)
                || string.IsNullOrWhiteSpace(client.LastName)
                || client.DateOfBirth == null)
            {
                return RedirectToPage("/Dashboard/Profile");
            }


            if (!ModelState.IsValid)
            {
                LoadLookups();
                return Page();
            }

            //Cr?ation automatique du OwnerProfile

            var ownerProfile = await _context.OwnerProfiles
        .FirstOrDefaultAsync(o => o.ApplicationUserId == user.Id);

            if (ownerProfile == null)
            {
                ownerProfile = new OwnerProfile
                {
                    ApplicationUserId = user.Id,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    StartDate = DateOnly.FromDateTime(DateTime.Today)
                };

                _context.OwnerProfiles.Add(ownerProfile);
                await _context.SaveChangesAsync();
            }
            //Cr?ation automatique du OwnerProfile

            


            await using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                var property = new Property
                {
                    AddressLine = Input.AddressLine,
                    City = Input.City,
                    Region = Input.Region,
                    PropertyTypeId = Input.PropertyTypeId,
                    PropertySize = Input.PropertySize,
                    NumBedrooms = Input.NumBedrooms,
                    NumBathrooms = Input.NumBathrooms,
                    NumCarspaces = Input.NumCarspaces,
                    Description = Input.Description
                };

                _context.Properties.Add(property);
                await _context.SaveChangesAsync();

                var ownerRoleId = await _context.RoleTypes
                .Where(r => r.Description == "Propriétaire principal")
                .Select(r => r.Id)
                .FirstAsync();

                var ownership = new Ownership
                {
                    PropertyId = property.Id,
                    OwnerProfileId = ownerProfile.Id,
                    RoleTypeId = ownerRoleId,
                    SharePercent = 100,
                    IsPrimary = true,
                    StartDate = DateOnly.FromDateTime(DateTime.Today)
                };

                _context.Ownerships.Add(ownership);
                await _context.SaveChangesAsync();



                var listing = new Listing
                {
                    PropertyId = property.Id,
                    ListingTypeId = Input.ListingTypeId,
                    ListingStatusId = Input.ListingStatusId,
                    Price = Input.Price,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Today),
                    DeletedAt = null
                };

                _context.Listings.Add(listing);
                await _context.SaveChangesAsync();

                



                // Historique initial
                await _priceService.CreateInitialPriceAsync(listing);

                await tx.CommitAsync();

                // Redirection vers Details du bien (qu'on fera ensuite)
                return RedirectToPage("Details", new { id = property.Id });
            }
            catch
            {
                await tx.RollbackAsync();
                ModelState.AddModelError(string.Empty, "Erreur lors de la cr?ation. Veuillez r?essayer.");
                LoadLookups();
                return Page();
            }
        }

        private void LoadLookups()
        {
            PropertyTypes = new SelectList(_context.PropertyTypes.AsNoTracking(), "Id", "Description");
            ListingTypes = new SelectList(_context.ListingTypes.AsNoTracking(), "Id", "Description");
            ListingStatuses = new SelectList(_context.ListingStatuses.AsNoTracking(), "Id", "Description");
        }
    }
}
