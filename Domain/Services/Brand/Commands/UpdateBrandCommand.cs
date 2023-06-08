using Domain.Models.Responses.Brand;
using MediatR;

namespace Domain.Services.Brand.Commands
{
    public class UpdateBrandCommand : IRequest<BrandResponse>
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; } = string.Empty;

        public UpdateBrandCommand(Guid brandId, string name)
        {
            BrandId = brandId;
            Name = name;
        }
    }
}
