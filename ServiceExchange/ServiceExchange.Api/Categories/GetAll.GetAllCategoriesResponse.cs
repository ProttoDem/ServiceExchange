namespace ServiceExchange.Api.Categories;

public class GetAllCategoriesResponse
{
    public IEnumerable<CategoryRecord> Categories { get; set; } = [];
}