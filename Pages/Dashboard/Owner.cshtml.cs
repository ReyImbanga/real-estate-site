using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Data;
using RealEstateWeb.Domain.Entities;
using System;

namespace RealEstateWeb.Pages.Dashboard
{
    [Authorize(Policy = "RequireOwnerProfile")]
    public class OwnerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OwnerModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Property> Properties { get; set; } = new List<Property>();
        public IList<Listing> Listings { get; set; } = new List<Listing>();
        public IList<Offer> Offers { get; set; } = new List<Offer>();

        public async Task OnGetAsync()
        {
            // 1️⃣ Utilisateur connecté
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return;

            // 2️⃣ OwnerProfile
            var owner = await _context.OwnerProfiles
                .FirstOrDefaultAsync(o => o.ApplicationUserId == user.Id);

            if (owner == null) return;

            // 3️⃣ Mes biens (via Ownership)
            Properties = await _context.Ownerships
                .Where(o => o.OwnerProfileId == owner.Id && o.EndDate == null)
                .Include(o => o.Property)
                    .ThenInclude(p => p.PropertyType)
                .Select(o => o.Property)
                .ToListAsync();


            var propertyIds = Properties.Select(p => p.Id).ToList();

            // 4️⃣ Mes annonces
            Listings = await _context.Listings
                .Where(l => propertyIds.Contains(l.PropertyId))
                .Include(l => l.Property)
                .Include(l => l.ListingStatus)
                .ToListAsync();

            // 5️⃣ Offres reçues
            Offers = await _context.Offers
                .Where(o => propertyIds.Contains(o.PropertyId))
                .Include(o => o.Property)
                .Include(o => o.ClientProfile)
                .Include(o => o.OfferStatus)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }
    }
}
