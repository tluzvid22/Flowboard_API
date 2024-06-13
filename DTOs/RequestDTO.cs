using Data.Enums;

namespace API.DTOs
{
    public record RequestDTO : AuditDto
    {
        public int UserId { get; set; }
        public PublicUserDTO User { get; set; }
        public int RequestedByUserId { get; set; }
    }
}
