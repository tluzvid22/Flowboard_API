namespace API.DTOs
{
    public record UserDTO : AuditDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;


        public ICollection<WorkspaceDTO> Workspaces { get; set; } = new List<WorkspaceDTO>();
        public FileDTO Image { get; set; }

        public int? TokenId { get; set; }
        public TokenDTO? Token { get; set; }
    }
}
