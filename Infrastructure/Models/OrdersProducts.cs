using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("orders_products")]
    public class OrdersProducts
    {
        [Key]
        public Guid OrderProductId { get; set; }
        public int Quantity { get; set; }
      
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        [ForeignKey("order")]
        public Guid OrderId { get; set; }
        [ForeignKey("product")]
        public Guid ProductId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Order? Order { get; set; }
    }
}
