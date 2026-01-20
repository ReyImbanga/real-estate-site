using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Data;

namespace RealEstateWeb.Pages.Properties
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Domain.Entities.Property Property { get; set; } = null!;

        public async Task<IActionResult> OnGet(int id)
        {
            Property = await _context.Properties
                .Include(p => p.PropertyType)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Property == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var property = await _context.Properties
                .Include(p => p.Listings)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (property == null)
                return NotFound();

            var now = DateTime.UtcNow;

            // Soft delete Property
            property.DeletedAt = now;

            // Soft delete Listings li?es
            foreach (var listing in property.Listings)
            {
                if (listing.DeletedAt == null)
                {
                    listing.DeletedAt = now;
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
