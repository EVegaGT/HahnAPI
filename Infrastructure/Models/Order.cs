using Infrastructure.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("orders")]
    [Index(nameof(NumberOrder), IsUnique = true)]
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        [Required(ErrorMessage = "A order number is required")]
        [MaxLength(20)]
        public string? NumberOrder { get; set; }
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "A total is required")]
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        [ForeignKey("user")]
        public Guid UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<OrdersProducts>? OrdersProducts { get; set; }
    }
}
