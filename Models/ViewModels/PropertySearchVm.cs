namespace RealEstateWeb.Models.ViewModels
{
    public class PropertySearchVm
    {
        // Filtres
        public string? City { get; set; }
        public int? PropertyTypeId { get; set; }
        public int? ListingTypeId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }

        // Résultats
        public List<SearchResultItem> Results { get; set; } = new();

        public class SearchResultItem
        {
            public int PropertyId { get; set; }
            public string AddressLine { get; set; } = null!;
            public string City { get; set; } = null!;
            public string PropertyType { get; set; } = null!;
            public string ListingType { get; set; } = null!;
            public int Price { get; set; }
        }
    }
}
