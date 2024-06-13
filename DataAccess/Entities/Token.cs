using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Token
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; } = string.Empty;

        [Required]
        public DateTime Expires { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
