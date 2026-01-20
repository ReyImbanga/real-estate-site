using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstateWeb.Data;
using RealEstateWeb.Domain.Entities;
using RealEstateWeb.Models.ViewModels;

namespace RealEstateWeb.Pages.Properties
{
    [Authorize(Policy = "RequireOwnerProfile")]
    public class UploadImagesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public UploadImagesModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public PropertyMediaUploadVm Input { get; set; } = null!;

        public IActionResult OnGet(int propertyId)
        {
            Input = new PropertyMediaUploadVm
            {
                PropertyId = propertyId
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Input.Files.Count == 0)
                return Page();

            var property = await _context.Properties.FindAsync(Input.PropertyId);
            if (property == null)
                return NotFound();

            var uploadRoot = Path.Combine(
                _env.WebRootPath,
                "uploads",
                "properties",
                Input.PropertyId.ToString());

            Directory.CreateDirectory(uploadRoot);

            foreach (var file in Input.Files)
            {
                if (!IsImage(file))
                    continue;

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var fullPath = Path.Combine(uploadRoot, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(stream);

                var media = new Media
                {
                    PropertyId = property.Id,
                    MediaTypeId = GetMediaTypeId(file),
                    MediaPath = $"/uploads/properties/{property.Id}/{fileName}",
                    CreatedAt = DateTime.UtcNow
                };

                _context.Medias.Add(media);
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("Details", new { id = Input.PropertyId });
        }

        private bool IsImage(IFormFile file)
        {
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowed.Contains(ext);
        }

        private int GetMediaTypeId(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            return ext switch
            {
                ".jpg" or ".jpeg" => 1,
                ".png" => 2,
                ".webp" => 2,
                _ => 1
            };
        }
    }
}
