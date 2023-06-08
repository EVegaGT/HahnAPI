using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;
using Domain.Models.Responses.Category;
using Infrastructure.Repositories.ReadOnlyRepositories;
using MediatR;

namespace Domain.Services.Category.Queries.Handlers
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryResponse>
    {
        private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(ICategoryReadOnlyRepository categoryReadOnlyRepository, IMapper mapper)
        {
            _categoryReadOnlyRepository = categoryReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<GetCategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.CategoryRequestIsRequired);

            try
            {
                var category = _mapper.Map<CategoryDto>(await _categoryReadOnlyRepository.GetCategoryById(request.CategoryId));
                return category != null ? _mapper.Map<GetCategoryResponse>(category) : throw new HahnApiException(ErrorCodeEnum.CategoryNotFound);
            }
            catch (HahnApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not get category by ID {request.CategoryId}. {ex.Message}");
            }
        }
    }
}
