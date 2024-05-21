namespace API.DTOs
{
    public record WorkspaceDTO : AuditDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<int> ListsIds { get; set; } = new List<int>();

        public int UserId { get; set; }
    }
}
