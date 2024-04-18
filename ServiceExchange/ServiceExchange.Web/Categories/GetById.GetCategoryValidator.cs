using FastEndpoints;
using FluentValidation;

namespace ServiceExchange.Web.Categories;

public class GetCategoryValidator : Validator<GetCategoryByIdRequest>
{
    public GetCategoryValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty().NotNull().WithMessage("CategoryId should not be empty!");
    }
}