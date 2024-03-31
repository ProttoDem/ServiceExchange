using Ardalis.Result;
using Ardalis.SharedKernel;
using ServiceExchange.Core.CategoryAggregate;

namespace Service.UseCases.Categories.Update;

public class UpdateCategoryHandler(IRepository<Category> _repository)
    : ICommandHandler<UpdateCategoryCommand, Result<CategoryDTO>>
{
    public async Task<Result<CategoryDTO>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await _repository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (existingCategory == null)
        {
            return Result.NotFound();
        }

        existingCategory.UpdateTitle(request.NewTitle!);
        existingCategory.UpdateDescription(request.NewDescription!);

        await _repository.UpdateAsync(existingCategory, cancellationToken);

        return Result.Success(new CategoryDTO(existingCategory.Id,
            existingCategory.Title, existingCategory.Description));
    }
}