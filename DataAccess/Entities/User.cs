﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Index(nameof(Username), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public record User : AuditEntity
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public string Phone { get; set; } = string.Empty;
    
    [Required]
    public string Country { get; set; } = string.Empty;

    [Required]
    public string State { get; set; } = string.Empty;

    [ForeignKey("Image")]
    public int ImageId { get; set; }
    public Files Image { get; set; }

    public ICollection<Workspace> Workspaces { get; set; } = new List<Workspace>();
    public ICollection<Collaborator> Collaborations { get; set; } = [];
    public virtual ICollection<Friend> FriendsUser1 { get; set; } = [];
    public virtual ICollection<Friend> FriendsUser2 { get; set; } = [];
    public virtual ICollection<Request> SenderRequestsUser1 { get; set; } = [];
    public virtual ICollection<Request> ReceiverRequestsUser2 { get; set; } = [];
    public virtual ICollection<UserTask> AssignedTasks { get; set; } = [];
    public ICollection<Request> Requests { get; set; } = new List<Request>();

    public int? TokenId { get; set; }
    public Token Token { get; set; }
}