namespace ServiceExchange.Api.Categories;

public class GetCategoryByIdRequest
{
    public const string Route = "/Categories/{CategoryId:Guid}";
    public static string BuildRoute(Guid categoryId) => Route.Replace("{CategoryId:Guid}", categoryId.ToString());

    public Guid CategoryId { get; set; }
}