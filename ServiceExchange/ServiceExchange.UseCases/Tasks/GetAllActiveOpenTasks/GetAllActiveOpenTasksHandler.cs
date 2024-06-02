using Ardalis.Result;
using Ardalis.SharedKernel;
using Service.UseCases.Tasks.GetAllByUser;
using ServiceExchange.Core.CalendarAggregate;
using ServiceExchange.Core.CalendarAggregate.Specifications;
using ServiceExchange.Core.CategoryAggregate;
using ServiceExchange.Core.CategoryAggregate.Specifications;
using ServiceExchange.Core.TaskAggregate.Specifications;

namespace Service.UseCases.Tasks.GetAllActiveOpenTasks;

public class GetAllActiveOpenTasksHandler(IReadRepository<ServiceExchange.Core.TaskAggregate.Task> _repository, IReadRepository<Calendar> _calendarRepository, IReadRepository<Category> _categoryRepository) : IQueryHandler<GetAllActiveOpenTasksQuery, Result<List<TaskDTO>>>
{
    public async Task<Result<List<TaskDTO>>> Handle(GetAllActiveOpenTasksQuery request, CancellationToken cancellationToken)
    {
        var spec = new TasksActiveOpenSpec();
        var tasks = await _repository.ListAsync(spec, cancellationToken);
        List<TaskDTO> result = new List<TaskDTO>();
        foreach (var task in tasks)
        {
            result.Add(new TaskDTO(task.Id, task.Title, task.Description, await _calendarRepository.FirstOrDefaultAsync(new CalendarByIdSpec(task.CalendarId)), await _categoryRepository.FirstOrDefaultAsync(new CategoryByIdSpec(task.CategoryId)), task.Price, task.Status, task.TaskUsers));
        }
        return Result.Success(result);
    }
}