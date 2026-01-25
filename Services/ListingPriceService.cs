using Microsoft.EntityFrameworkCore;
using RealEstateWeb.Data;
using RealEstateWeb.Domain.Entities;

namespace RealEstateWeb.Services
{
    public class ListingPriceService
    {
        private readonly ApplicationDbContext _context;

        public ListingPriceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateInitialPriceAsync(Listing listing)
        {
            var history = new ListingPriceHistory
            {
                ListingId = listing.Id,
                Price = listing.Price,
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = null
            };

            _context.ListingPriceHistories.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePriceIfChangedAsync(Listing listing, int newPrice)
        {
            if (listing.Price == newPrice)
                return;

            var today = DateOnly.FromDateTime(DateTime.Today);

            var current = await _context.ListingPriceHistories
                .FirstOrDefaultAsync(h =>
                    h.ListingId == listing.Id &&
                    h.EndDate == null);

            if (current != null)
            {
                current.EndDate = today;
            }

            var newHistory = new ListingPriceHistory
            {
                ListingId = listing.Id,
                Price = newPrice,
                StartDate = today,
                EndDate = null
            };

            listing.Price = newPrice;

            _context.ListingPriceHistories.Add(newHistory);
            await _context.SaveChangesAsync();
        }

    }
}
