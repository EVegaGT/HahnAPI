using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public interface IBrandRepository
    {
        Task CreateBrand(Brand brand);
        Task UpdateBrand(Brand brand);
    }
}