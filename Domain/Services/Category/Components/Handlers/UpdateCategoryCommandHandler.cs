using Domain.Models.Responses.Category;
using Infrastructure.Repositories.ReadOnlyRepositories;
using Infrastructure.Repositories;
using MediatR;
using Domain.Common.Exceptions;

namespace Domain.Services.Category.Components.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;

        public UpdateCategoryCommandHandler(ICategoryRepository CategoryRepository, ICategoryReadOnlyRepository categoryReadOnlyRepository)
        {
            _categoryRepository = CategoryRepository;
            _categoryReadOnlyRepository = categoryReadOnlyRepository;
        }
        public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.CategoryRequestIsRequired);
            if (request.CategoryId == Guid.Empty) throw new HahnApiException(ErrorCodeEnum.InvalidCategoryId);

            try
            {
                var category = await _categoryReadOnlyRepository.GetCategoryById(request.CategoryId) ?? throw new HahnApiException(ErrorCodeEnum.CategoryNotFound);
                category.Name = request.Name;
                await _categoryRepository.UpdateCategory(category);
                return new CategoryResponse(category.CategoryId);
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not update the Category. {ex.Message}");
            }
        }
    }
}
