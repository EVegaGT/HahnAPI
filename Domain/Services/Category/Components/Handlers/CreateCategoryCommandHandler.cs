using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Models.Dtos;
using Domain.Models.Responses.Category;
using Infrastructure.Repositories;
using MediatR;

namespace Domain.Services.Category.Components.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository CategoryRepository, IMapper mapper)
        {
            _CategoryRepository = CategoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new HahnApiException(ErrorCodeEnum.CategoryRequestIsRequired);

            try
            {
                if (request.CategoryId == Guid.Empty) request.CategoryId = Guid.NewGuid();

                var categoryDto = _mapper.Map<CategoryDto>(request);
                await _CategoryRepository.CreateCategory(_mapper.Map<Infrastructure.Models.Category>(categoryDto));
                return new CategoryResponse(categoryDto.CategoryId);
            }
            catch (Exception ex)
            {
                throw new HahnApiException(ErrorCodeEnum.InvalidRequest, $"Could not create Category. {ex.Message}");
            }
        }
    }
}
