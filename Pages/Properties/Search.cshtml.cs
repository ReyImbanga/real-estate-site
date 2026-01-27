using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Data;
using RealEstateWeb.Models.ViewModels;

namespace RealEstateWeb.Pages.Properties
{
    public class SearchModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SearchModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public PropertySearchVm Input { get; set; } = new();

        public SelectList PropertyTypes { get; set; } = null!;
        public SelectList ListingTypes { get; set; } = null!;

        public async Task OnGetAsync()
        {
            LoadLookups();

            var query = _context.Listings
                .AsNoTracking()
                .Where(l => l.DeletedAt == null)
                .Include(l => l.Property)
                    .ThenInclude(p => p.PropertyType)
                .Include(l => l.ListingType)
                .Include(l => l.ListingStatus)
                .Where(l => l.ListingStatus.Description == "Actif");

            // Filtres dynamiques
            if (!string.IsNullOrWhiteSpace(Input.City))
                query = query.Where(l =>
                    l.Property.City.ToLower().Contains(Input.City.ToLower()));

            if (Input.PropertyTypeId.HasValue)
                query = query.Where(l =>
                    l.Property.PropertyTypeId == Input.PropertyTypeId);

            if (Input.ListingTypeId.HasValue)
                query = query.Where(l =>
                    l.ListingTypeId == Input.ListingTypeId);

            if (Input.MinPrice.HasValue)
                query = query.Where(l =>
                    l.Price >= Input.MinPrice.Value);

            if (Input.MaxPrice.HasValue)
                query = query.Where(l =>
                    l.Price <= Input.MaxPrice.Value);

            Input.Results = await query
                .OrderByDescending(l => l.CreatedDate)
                .Select(l => new PropertySearchVm.SearchResultItem
                {
                    PropertyId = l.Property.Id,
                    AddressLine = l.Property.AddressLine,
                    City = l.Property.City,
                    PropertyType = l.Property.PropertyType.Description,
                    ListingType = l.ListingType.Description,
                    Price = l.Price
                })
                .ToListAsync();
        }

        private void LoadLookups()
        {
            PropertyTypes = new SelectList(
                _context.PropertyTypes.AsNoTracking(),
                "Id",
                "Description");

            ListingTypes = new SelectList(
                _context.ListingTypes.AsNoTracking(),
                "Id",
                "Description");
        }
    }
}
