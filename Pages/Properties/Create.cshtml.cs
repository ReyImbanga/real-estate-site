using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "RequireOwnerProfile")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ListingPriceService _priceService;

        public CreateModel(ApplicationDbContext context, ListingPriceService priceService)
        {
            _context = context;
            _priceService = priceService;
        }

        [BindProperty]
        public PropertyListingCreateVm Input { get; set; } = new();

        public SelectList PropertyTypes { get; set; } = null!;
        public SelectList ListingTypes { get; set; } = null!;
        public SelectList ListingStatuses { get; set; } = null!;

        public void OnGet()
        {
            LoadLookups();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadLookups();
                return Page();
            }

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
