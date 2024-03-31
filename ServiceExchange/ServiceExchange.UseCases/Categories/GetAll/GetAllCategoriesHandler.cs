using Ardalis.Result;
using Ardalis.SharedKernel;
using ServiceExchange.Core.CategoryAggregate;

namespace Service.UseCases.Categories.GetAll;

public class GetAllCategoriesHandler(IReadRepository<Category> _repository)
    : IQueryHandler<GetAllCategoriesQuery, Result<List<CategoryDTO>>>
{
    public async Task<Result<List<CategoryDTO>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _repository.ListAsync();

        var result = categories.Select(cat => new CategoryDTO(cat.Id, cat.Title, cat.Description)).ToList();

        return Result.Success(result);
    }
}