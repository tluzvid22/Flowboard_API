namespace API.DTOs
{
    public record PublicUserDTO : AuditDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public FileDTO Image { get; set; }
    }
}
