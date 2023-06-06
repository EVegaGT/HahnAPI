using Domain.Models.Responses.Brand;
using MediatR;

namespace Domain.Services.Brand.Commands
{
    public class CreateBrandCommand : IRequest<BrandResponse>
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
