using Ardalis.Specification;

namespace ServiceExchange.Core.UserAggregate.Specifications;

public class UserByIdSpec : Specification<User>
{
    public UserByIdSpec(string systemId)
    {
        Query.Where(user => user.SystemId == systemId);
    }
}