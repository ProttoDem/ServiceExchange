using Ardalis.Specification;

namespace ServiceExchange.Core.RoleAggregate.Specifications;

public class RoleByIdSpec: Specification<Role>
{
    public RoleByIdSpec(Guid roleId)
    {
        Query.Where(role => role.Id == roleId);
    }
}