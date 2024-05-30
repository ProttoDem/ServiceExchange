using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Service.UseCases.Users.Get;

public record GetUserBySystemIdQuery(string SystemId) : IQuery<Result<List<UserDTO>>>;