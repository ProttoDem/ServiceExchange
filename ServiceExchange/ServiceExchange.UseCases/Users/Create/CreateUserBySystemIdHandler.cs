using Ardalis.Result;
using Ardalis.SharedKernel;
using ServiceExchange.Core.UserAggregate;

namespace Service.UseCases.Users.Create;

public class CreateUserBySystemIdHandler(IRepository<User> _repository)
    : ICommandHandler<CreateUserBySystemIdCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateUserBySystemIdCommand command, CancellationToken cancellationToken)
    {
        var newUser = new User(command.SystemId);
       
        var createdItem = await _repository.AddAsync(newUser, cancellationToken);

        return createdItem.Id;
    }
}