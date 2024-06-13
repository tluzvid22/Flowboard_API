using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public record Collaborator : AuditEntity
    {
        [Required, ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }        

        [Required, ForeignKey("Workspace")]
        public int WorkspaceId { get; set; }
        public virtual Workspace Workspace { get; set; }

        [Required]
        public bool CanModify { get; set; } = false;
        [Required]
        public bool CanDelete { get; set; } = false;
        [Required]
        public bool CanRead { get; set; } = true;
        [Required]
        public bool IsAdmin { get; set; } = false;
    }
}
