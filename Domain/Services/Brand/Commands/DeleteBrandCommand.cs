using MediatR;

namespace Domain.Services.Brand.Commands
{
    public class DeleteBrandCommand : IRequest<bool>
    {
        public Guid BrandId { get; set; }

        public DeleteBrandCommand(Guid brandId) => BrandId = brandId;
       
    }
}