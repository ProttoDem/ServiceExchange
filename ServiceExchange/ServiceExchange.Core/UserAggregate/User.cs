using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using ServiceExchange.Core.RoleAggregate;
using ServiceExchange.Core.TaskUserAggreagate;

namespace ServiceExchange.Core.UserAggregate;

public class User(string? firstName, string? lastName) : BaseEntity<Guid>, IAggregateRoot
{
    public string? FirstName { get; private set; } = firstName;
    public string? LastName { get; private set; } = lastName;
    public List<Role> Roles { get; } = [];
    public List<TaskUser> TaskUsers { get; } = [];

    public void UpdateFirstName(string firstName)
    {
        FirstName = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
    }
    
    public void UpdateLastName(string lastName)
    {
        LastName = Guard.Against.NullOrEmpty(lastName, nameof(lastName));
    }
}