using Ardalis.Result;
using Ardalis.SharedKernel;
using ServiceExchange.Core.UserAggregate;
using ServiceExchange.Core.UserAggregate.Specifications;

namespace Service.UseCases.Users.Get;

public class GetUserBySystemIdHandler(IReadRepository<User> _repository)
    : IQueryHandler<GetUserBySystemIdQuery, Result<UserDTO>>
{
    public async Task<Result<UserDTO>> Handle(GetUserBySystemIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new UserByIdSpec(request.SystemId);
        var user = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (user == null)
        {
            return Result.NotFound();
        }
        var result = new UserDTO(user.SystemId);

        return Result.Success(result);
    }
}