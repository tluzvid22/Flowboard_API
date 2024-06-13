namespace API.DTOs
{
    public record CollaboratorDTO : AuditDto
    {
        public int UserId { get; set; }
        public PublicUserDTO User { get; set; }
        public int WorkspaceId { get; set; }
        public WorkspaceDTO Workspace { get; set; }

        public bool CanModify { get; set; } = false;
        public bool CanDelete { get; set; } = false;
        public bool CanRead { get; set; } = true;
        public bool IsAdmin { get; set; } = false;
    }
}
