namespace RealEstateWeb.Domain.Entities
{
    public class ContractStatus
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;

        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
