using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Service.UseCases.Tasks.GetAllByUser;

public record GetAllTasksByUserQuery(string UserId) : IQuery<Result<List<TaskDTO>>>;