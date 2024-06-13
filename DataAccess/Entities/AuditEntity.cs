using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public abstract record AuditEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}