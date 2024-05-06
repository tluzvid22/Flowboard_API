using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public record ImageDTO : AuditDto
    {
        public int Id { get; set; }

        public byte[] File { get; set; } = [];
    }
}