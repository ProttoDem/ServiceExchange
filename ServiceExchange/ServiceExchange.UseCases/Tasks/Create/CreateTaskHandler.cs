using Ardalis.Result;
using Ardalis.SharedKernel;
using ServiceExchange.Core.CalendarAggregate;
using ServiceExchange.Core.CategoryAggregate;
using ServiceExchange.Core.CategoryAggregate.Specifications;
using ServiceExchange.Core.StatusAggregate;
using ServiceExchange.Core.TaskUserAggreagate;
using ServiceExchange.Core.UserAggregate;
using ServiceExchange.Core.UserAggregate.Specifications;
using Task = ServiceExchange.Core.TaskAggregate.Task;

namespace Service.UseCases.Tasks.Create;

public class CreateTaskHandler(IRepository<Task> _taskRepository, IReadRepository<Category> _categoryRepository, IReadRepository<User> _userRepository)
    : ICommandHandler<CreateTaskCommand, Result<TaskDTO>>
{
    public async Task<Result<TaskDTO>> Handle(CreateTaskCommand request,
        CancellationToken cancellationToken)
    {
        var categorySpec = new CategoryByIdSpec(request.CategoryId);
        var category = await _categoryRepository.FirstOrDefaultAsync(categorySpec, cancellationToken);
        if (category == null) return Result.NotFound("Category wasn't found.");
        
        var userSpec = new UserByIdSpec(request.UserId);
        var user = await _userRepository.FirstOrDefaultAsync(userSpec, cancellationToken);
        if (user == null) return Result.NotFound("User wasn't found.");
        
        var newTask = new Task(request.Title, request.Description, request.Price, Status.None);
        newTask.Category = category;
        newTask.Calendar = new Calendar{StartTime = request.StartTime, EndTime = request.FinishTime, IsRepeatable = request.IsRepeatable};
        newTask.TaskUsers.Add(new TaskUser(){ User = user, IsOwner = true});
       
        var createdItem = await _taskRepository.AddAsync(newTask, cancellationToken);

        return new TaskDTO(createdItem.Id, createdItem.Title, createdItem.Description, createdItem.Calendar, createdItem.Category, createdItem.Price, createdItem.Status, createdItem.TaskUsers);
    }
}