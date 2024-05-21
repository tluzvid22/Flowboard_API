using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public abstract record AuditEntity
{
    public DateTime CreatedAt { get; set; }

    [MaxLength(50)]
    public string CreatedBy { get; set; } = string.Empty;

    public DateTime? UpdatedAt { get; set; }

    [MaxLength(50)]
    public string? UpdatedBy { get; set; }
}