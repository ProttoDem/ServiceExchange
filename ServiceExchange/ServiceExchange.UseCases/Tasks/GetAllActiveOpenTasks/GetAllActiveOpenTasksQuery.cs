using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Service.UseCases.Tasks.GetAllActiveOpenTasks;

public record GetAllActiveOpenTasksQuery() : IQuery<Result<List<TaskDTO>>>;