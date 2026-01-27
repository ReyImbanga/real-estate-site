namespace RealEstateWeb.Domain.Entities
{
    public class RoleType
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public ICollection<Ownership> Ownerships { get; set; } = new List<Ownership>();
    }
}
