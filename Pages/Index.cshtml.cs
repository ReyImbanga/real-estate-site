using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Data;
using RealEstateWeb.Domain.Entities;

namespace RealEstateWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Listing> Listings { get; set; } = new();
        public async Task OnGetAsync()
        {
            Listings = await _context.Listings
           .Where(l => l.DeletedAt == null)
           .Include(l => l.Property)
               .ThenInclude(p => p.PropertyType)
           .Include(l => l.ListingType)
           .Include(l => l.ListingStatus)
           .OrderByDescending(l => l.CreatedDate)
           .ToListAsync();
        }
    }
}
