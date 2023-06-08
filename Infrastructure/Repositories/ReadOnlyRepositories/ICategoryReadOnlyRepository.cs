using Infrastructure.Models;

namespace Infrastructure.Repositories.ReadOnlyRepositories
{
    public interface ICategoryReadOnlyRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category?> GetCategoryById(Guid categoryId);
        Task<bool> ExistCategoryById(Guid categoryId);
    }
}