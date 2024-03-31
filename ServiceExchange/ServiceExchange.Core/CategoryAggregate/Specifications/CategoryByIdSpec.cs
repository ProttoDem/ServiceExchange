using Ardalis.Specification;

namespace ServiceExchange.Core.CategoryAggregate.Specifications;

public class CategoryByIdSpec : Specification<Category>
{
    public CategoryByIdSpec(Guid categoryId)
    {
        Query.Where(category => category.Id == categoryId);
    }
}