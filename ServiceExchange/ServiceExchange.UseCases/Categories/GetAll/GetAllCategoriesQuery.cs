using Ardalis.Result;
using Ardalis.SharedKernel;
using Service.UseCases.Tasks;

namespace Service.UseCases.Categories.GetAll;

public record GetAllCategoriesQuery(int? Skip, int? Take) : IQuery<Result<List<CategoryDTO>>>;