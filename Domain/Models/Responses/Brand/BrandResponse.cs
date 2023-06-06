namespace Domain.Models.Responses.Brand
{
    public class BrandResponse
    {
        public Guid BrandId { get; set; }
        public BrandResponse(Guid brandId) => BrandId = brandId;
    }
}
