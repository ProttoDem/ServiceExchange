using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Service.UseCases.Users.Create;

public record CreateUserBySystemIdCommand(string SystemId) : Ardalis.SharedKernel.ICommand<Result<Guid>>;