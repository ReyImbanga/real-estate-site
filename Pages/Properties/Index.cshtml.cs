using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Data;
using RealEstateWeb.Domain.Entities;


namespace RealEstateWeb.Pages.Properties
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Property> Properties { get; set; } = new List<Property>();

        public async Task OnGetAsync()
        {
            Properties = await _context.Properties
                .Include(p => p.PropertyType)
                .ToListAsync();
        }
    }
}
