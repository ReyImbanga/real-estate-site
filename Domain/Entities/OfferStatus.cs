namespace RealEstateWeb.Domain.Entities
{
    public class OfferStatus
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;

        public ICollection<Offer> Offers { get; set; } = new List<Offer>();
    }
}
