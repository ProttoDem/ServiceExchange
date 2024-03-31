using Ardalis.Result;
using Ardalis.SharedKernel;
namespace Service.UseCases.Categories.Update;

public record UpdateCategoryCommand(Guid CategoryId, string? NewTitle, string? NewDescription) : ICommand<Result<CategoryDTO>>;