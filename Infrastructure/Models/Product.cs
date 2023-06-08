using Infrastructure.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "A Name is required")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "A Quantity is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "A Price is required")]
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        [ForeignKey("brand")]
        public Guid BrandId { get; set; }
        [ForeignKey("category")]
        public Guid CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual ICollection<OrdersProducts>? OrdersProducts { get; set; }
    }
}
