namespace Domain.Models.Responses.Category
{
    public class CategoryResponse
    {
        public Guid CategoryId { get; set; }
        public CategoryResponse(Guid brandId) => CategoryId = brandId;
    }
}
