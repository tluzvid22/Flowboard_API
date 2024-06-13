using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public record Friend : AuditEntity
{
    [Required, ForeignKey("User1")]
    public int User1Id { get; set; }
    [Required, ForeignKey("User2")]
    public int User2Id { get; set; }

    public virtual User User1 { get; set; }
    public virtual User User2 { get; set; }

}