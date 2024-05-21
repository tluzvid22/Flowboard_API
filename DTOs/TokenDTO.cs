using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs
{
    public record TokenDTO : AuditDto
    {
        public int Id { get; set; }


        public string Value { get; set; } = string.Empty;


        public DateTime ExpiryDate { get; set; } 


        public int UserId { get; set; }
        public UserDTO User { get; set; }
    }
}
