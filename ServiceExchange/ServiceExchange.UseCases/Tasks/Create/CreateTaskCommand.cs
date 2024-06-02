using Ardalis.Result;

namespace Service.UseCases.Tasks.Create;

public record CreateTaskCommand(string UserId, string Title, string? Description, DateTime StartTime, DateTime? FinishTime, double Price, Guid CategoryId, bool? IsRepeatable) : Ardalis.SharedKernel.ICommand<Result<TaskDTO>>;