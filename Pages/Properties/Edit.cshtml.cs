using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Data;
using RealEstateWeb.Domain.Entities;
using RealEstateWeb.Models.ViewModels;

namespace RealEstateWeb.Pages.Properties
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PropertyListingEditVm Input { get; set; } = null!;

        public SelectList PropertyTypes { get; set; } = null!;
        public SelectList ListingTypes { get; set; } = null!;
        public SelectList ListingStatuses { get; set; } = null!;

        public async Task<IActionResult> OnGet(int id)
        {
            var property = await _context.Properties
                .Include(p => p.Listings)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (property == null)
                return NotFound();

            var listing = property.Listings
                .OrderByDescending(l => l.CreatedDate)
                .FirstOrDefault();

            if (listing == null)
                return NotFound("Aucune annonce associ?e ? ce bien.");

            Input = new PropertyListingEditVm
            {
                PropertyId = property.Id,
                ListingId = listing.Id,
                AddressLine = property.AddressLine,
                City = property.City,
                Region = property.Region,
                PropertyTypeId = property.PropertyTypeId,
                PropertySize = property.PropertySize,
                NumBedrooms = property.NumBedrooms,
                NumBathrooms = property.NumBathrooms,
                NumCarspaces = property.NumCarspaces,
                Description = property.Description,
                ListingTypeId = listing.ListingTypeId,
                ListingStatusId = listing.ListingStatusId,
                Price = listing.Price
            };

            LoadLookups();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadLookups();
                return Page();
            }

            var property = await _context.Properties
                .FirstOrDefaultAsync(p => p.Id == Input.PropertyId);

            var listing = await _context.Listings
                .FirstOrDefaultAsync(l => l.Id == Input.ListingId);

            if (property == null || listing == null)
                return NotFound();

            // Mise ? jour Property
            property.AddressLine = Input.AddressLine;
            property.City = Input.City;
            property.Region = Input.Region;
            property.PropertyTypeId = Input.PropertyTypeId;
            property.PropertySize = Input.PropertySize;
            property.NumBedrooms = Input.NumBedrooms;
            property.NumBathrooms = Input.NumBathrooms;
            property.NumCarspaces = Input.NumCarspaces;
            property.Description = Input.Description;

            // Mise ? jour Listing
            listing.ListingTypeId = Input.ListingTypeId;
            listing.ListingStatusId = Input.ListingStatusId;
            listing.Price = Input.Price;

            await _context.SaveChangesAsync();

            return RedirectToPage("Details", new { id = property.Id });
        }

        private void LoadLookups()
        {
            PropertyTypes = new SelectList(_context.PropertyTypes, "Id", "Description");
            ListingTypes = new SelectList(_context.ListingTypes, "Id", "Description");
            ListingStatuses = new SelectList(_context.ListingStatuses, "Id", "Description");
        }
    }
}
