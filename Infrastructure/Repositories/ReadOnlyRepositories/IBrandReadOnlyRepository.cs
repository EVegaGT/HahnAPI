using Infrastructure.Models;

namespace Infrastructure.Repositories.ReadOnlyRepositories
{
    public interface IBrandReadOnlyRepository
    {
        Task<Brand?> GetBrandById(Guid brandId);
        Task<List<Brand>> GetBrands();
    }
}