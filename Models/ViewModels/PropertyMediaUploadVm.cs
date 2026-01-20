using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealEstateWeb.Models.ViewModels
{
    public class PropertyMediaUploadVm
    {
        public int PropertyId { get; set; }

        [Required]
        public List<IFormFile> Files { get; set; } = new();
    }
}
