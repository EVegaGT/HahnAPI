using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public interface ICategoryRepository
    {
        Task CreateCategory(Category category);
        Task UpdateCategory(Category category);
    }
}