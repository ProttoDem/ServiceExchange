using Ardalis.Result;
using Ardalis.SharedKernel;
using ServiceExchange.Core.CategoryAggregate;
using ServiceExchange.Core.CategoryAggregate.Specifications;

namespace Service.UseCases.Categories.Get;

public class GetCategoryHandler(IReadRepository<Category> _repository)
    : IQueryHandler<GetCategoryQuery, Result<CategoryDTO>>
{
    public async Task<Result<CategoryDTO>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var spec = new CategoryByIdSpec(request.CategoryId);
        var entity = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (entity == null) return Result.NotFound();

        return new CategoryDTO(entity.Id, entity.Title, entity.Description);
    }
}