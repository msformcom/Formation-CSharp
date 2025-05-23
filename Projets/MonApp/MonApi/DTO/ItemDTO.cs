namespace MonApi.DTO
{
    public class ItemDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Libelle { get; set; }
        public Decimal? Prix { get; set; }
        public int? Points { get; set; }
        public string Description { get; set; }
        public DateTime? DateRealisation { get; set; }
    }
}
