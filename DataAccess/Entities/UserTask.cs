using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public record UserTask : AuditEntity
    {
        [Required, ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }        

        [Required, ForeignKey("Task")]
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}
