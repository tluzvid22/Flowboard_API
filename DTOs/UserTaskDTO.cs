namespace API.DTOs
{
    public record UserTaskDTO : AuditDto
    {
        public int UserId { get; set; }
        public PublicUserDTO User { get; set; }
        public int TaskId { get; set; }
        public TaskDTO Tasks { get; set; }
    }
}
