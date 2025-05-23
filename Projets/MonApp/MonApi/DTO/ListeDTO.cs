namespace MonApi.DTO
{
    public class ListeDTO
    {
        public Guid? Id { get; set; }
        public string Libelle { get; set; }

        public IEnumerable<ItemDTO> Items { get; set; }
    }
}
