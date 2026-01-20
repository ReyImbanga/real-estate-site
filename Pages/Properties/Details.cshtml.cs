using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Data;
using RealEstateWeb.Domain.Entities;

namespace RealEstateWeb.Pages.Properties
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Property Property { get; set; } = null!;

        public async Task<IActionResult> OnGet(int id)
        {
            Property = await _context.Properties
                .Include(p => p.PropertyType)
                .Include(p => p.Listings)
                    .ThenInclude(l => l.ListingType)
                .Include(p => p.Listings)
                    .ThenInclude(l => l.ListingStatus)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Property == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
