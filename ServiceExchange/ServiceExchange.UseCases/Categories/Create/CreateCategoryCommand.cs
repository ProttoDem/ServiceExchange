using Ardalis.Result;

namespace Service.UseCases.Categories.Create;

public record CreateCategoryCommand(string Title, string? Description) : Ardalis.SharedKernel.ICommand<Result<Guid>>;