using Ardalis.Result;
using Ardalis.SharedKernel;
using Service.UseCases.Categories;
using Service.UseCases.Categories.GetAll;
using ServiceExchange.Core.TaskAggregate.Specifications;

namespace Service.UseCases.Tasks.GetAllByUser;

public class GetAllTasksByUserHandler(IReadRepository<ServiceExchange.Core.TaskAggregate.Task> _repository)
    : IQueryHandler<GetAllTasksByUserQuery, Result<List<TaskDTO>>>
{
    public async Task<Result<List<TaskDTO>>> Handle(GetAllTasksByUserQuery request, CancellationToken cancellationToken)
    {
        var spec = new TasksByUserIdSpec(request.UserId);
        var tasks = await _repository.ListAsync(spec, cancellationToken);

        var result = tasks.Select(task => new TaskDTO(task.Id, task.Title, task.Description, task.Calendar, task.Category, task.Price, task.Status, task.TaskUsers)).ToList();

        return Result.Success(result);
    }
}