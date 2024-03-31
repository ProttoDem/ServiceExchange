using System.ComponentModel;
using Ardalis.Result;
using Ardalis.SharedKernel;
using ServiceExchange.Core.CategoryAggregate;

namespace Service.UseCases.Categories.Create;

public class CreateCategoryHandler(IRepository<Category> _repository)
    : ICommandHandler<CreateCategoryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var newCategory = new Category(request.Title, request.Description);
       
        var createdItem = await _repository.AddAsync(newCategory, cancellationToken);

        return createdItem.Id;
    }
}