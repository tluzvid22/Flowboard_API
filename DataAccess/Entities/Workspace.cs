using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public record Workspace : AuditEntity
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public ICollection<List> Lists { get; set; } = [];

    public ICollection<Collaborator> Collaborator { get; set; } = [];

    [Required, ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
}