namespace API.DTOs
{
    public record FileDTO : AuditDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string FileType { get; set; } = string.Empty; public string Description { get; set; } = string.Empty;

        public string ContentUrl { get; set; } = string.Empty;

        public int? TaskId { get; set; }
        public TaskDTO Task { get; set; }
    }
}
