namespace API.DTOs
{
    public record ListDTO : AuditDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public ICollection<int> TasksIds { get; set; } = new List<int>();

        public int WorkspaceId { get; set; }
    }
}
