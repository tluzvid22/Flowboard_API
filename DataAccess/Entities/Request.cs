using Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public record Request : AuditEntity
{
    [Required, ForeignKey("Sender")]
    public int SenderId { get; set; }
    [Required, ForeignKey("Receiver")]
    public int ReceiverId { get; set; }

    [Required]
    public Status Status { get; set; }

    [Required]
    public int RequestedByUserId { get; set; }

    public virtual User Sender { get; set; }
    public virtual User Receiver { get; set; }
}