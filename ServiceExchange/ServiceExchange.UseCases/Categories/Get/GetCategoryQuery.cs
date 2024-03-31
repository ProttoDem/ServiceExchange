using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Service.UseCases.Categories.Get;

public record GetCategoryQuery(Guid CategoryId) : IQuery<Result<CategoryDTO>>;