using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public record Files : AuditEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string FileType { get; set; } = string.Empty;

    [Required]
    public byte[] File { get; set; } = [];

    [ForeignKey("Task")]
    public int? TaskId { get; set; }
    public Task Task { get; set; }

}