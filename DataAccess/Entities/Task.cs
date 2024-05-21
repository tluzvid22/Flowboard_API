using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public record Task : AuditEntity
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }

    [Required, ForeignKey("List")]
    public int ListId { get; set; }
    public List List { get; set; }


    public ICollection<Files> Files { get; set; } = new List<Files>();

    public ICollection<User> AssignedUsers { get; set; } = new List<User>();

}