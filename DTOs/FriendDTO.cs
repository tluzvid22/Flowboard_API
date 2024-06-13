namespace API.DTOs
{
    public record FriendDTO : AuditDto
    {
        public int UserId { get; set; }
        public PublicUserDTO User { get; set; }
    }
}
