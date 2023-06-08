using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;
using Domain.Models.Responses.Category;
using Infrastructure.Repositories.ReadOnlyRepositories;
using MediatR;

namespace Domain.Services.Category.Queries.Handlers
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<GetCategoryResponse>>
    {
        private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(ICategoryReadOnlyRepository categoryReadOnlyRepository, IMapper mapper)
        {
            _categoryReadOnlyRepository = categoryReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<List<GetCategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.CategoryRequestIsRequired);

            try
            {
                var categories = _mapper.Map<List<CategoryDto>>(await _categoryReadOnlyRepository.GetCategories());
                return categories == null ? throw new HahnApiException(ErrorCodeEnum.CategoryNotFound) : _mapper.Map<List<GetCategoryResponse>>(categories);
            }
            catch (HahnApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not get categories. {ex.Message}");
            }
        }
    }
}
