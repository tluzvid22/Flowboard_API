namespace API.DTOs
{
    public record WorkspaceDTO : AuditDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ListDTO[] Lists { get; set; } = [];

        public int UserId { get; set; }
    }
}
