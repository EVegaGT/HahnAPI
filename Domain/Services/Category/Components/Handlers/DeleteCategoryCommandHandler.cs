using Infrastructure.Repositories.ReadOnlyRepositories;
using Infrastructure.Repositories;
using MediatR;
using Domain.Common.Exceptions;

namespace Domain.Services.Category.Components.Handlers
{
    public class DeleteCategoryCommandHandler: IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository CategoryRepository, ICategoryReadOnlyRepository categoryReadOnlyRepository)
        {
            _categoryRepository = CategoryRepository;
            _categoryReadOnlyRepository = categoryReadOnlyRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.CategoryRequestIsRequired);
            if (request.CategoryId == Guid.Empty) throw new HahnApiException(ErrorCodeEnum.InvalidCategoryId);

            try
            {
                var category = await _categoryReadOnlyRepository.GetCategoryById(request.CategoryId) ?? throw new HahnApiException(ErrorCodeEnum.CategoryNotFound);
                category.IsDeleted = true;
                await _categoryRepository.UpdateCategory(category);
                return true;
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not delete the Category. {ex.Message}");
            }
        }
    }
}
