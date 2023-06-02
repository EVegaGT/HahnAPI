using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("roles")]
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }
        [Required(ErrorMessage = "A Name is required")]
        [MaxLength(25)]
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}