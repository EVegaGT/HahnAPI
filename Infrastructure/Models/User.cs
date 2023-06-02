﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "A Name is required")]
        [MaxLength(250)]
        public string? Name { get; set; }
        [MaxLength(250)]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "An Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        [ForeignKey("role")]
        public Guid RoleId { get; set; }
       
        public virtual Role? Role { get; set; }
       
    }
}