using FastEndpoints;
using FluentValidation;

namespace ServiceExchange.Api.Categories;

public class GetCategoryValidator : Validator<GetCategoryByIdRequest>
{
    public GetCategoryValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty().NotNull().WithMessage("CategoryId should not be empty!");
    }
}