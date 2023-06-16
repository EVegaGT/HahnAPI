
namespace Domain.Models.Requests.Order
{
    public class PatchProductOrderRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}