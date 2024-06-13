using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public record List : AuditEntity
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(25)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int Order { get; set; }

    public ICollection<Task> Tasks { get; set; } = new List<Task>();

    [Required, ForeignKey("Workspace")]
    public int WorkspaceId { get; set; }
    public Workspace Workspace { get; set; }
}