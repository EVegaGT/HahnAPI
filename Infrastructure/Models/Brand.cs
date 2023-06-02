using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("brands")]
    public class Brand
    {
        [Key]
        public Guid BrandId { get; set; }
        [Required(ErrorMessage = "A Name is required")]
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
