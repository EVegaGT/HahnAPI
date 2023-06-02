using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("categories")]
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "A Name is required")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
