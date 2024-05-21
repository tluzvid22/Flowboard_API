namespace API.DTOs
{
    public record TaskDTO : AuditDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }

        public int ListId { get; set; }
        public ListDTO List { get; set; }

        public ICollection<int> FilesIds { get; set; } = new List<int>();

        public ICollection<UserDTO> AssignedUsers { get; set; } = new List<UserDTO>();
    }
}
