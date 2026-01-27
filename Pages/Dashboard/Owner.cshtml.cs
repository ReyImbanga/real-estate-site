using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstateWeb.Data;
using RealEstateWeb.Domain.Entities;
using System;

namespace RealEstateWeb.Pages.Dashboard
{
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
        //public IList<Offer> Offers { get; set; } = new List<Offer>();

        public void OnGet()
        {
        }
    }
}
