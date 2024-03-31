using FastEndpoints;
using FluentValidation;
using ServiceExchange.Infrastructure.Data.Config;

namespace ServiceExchange.Categories;

public class CreateCategoryValidator : Validator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MinimumLength(DataSchemaConstants.DEFAULT_CATEGORY_NAME_MIN_LENGTH)
            .MaximumLength(DataSchemaConstants.DEFAULT_CATEGORY_NAME_MAX_LENGTH);
    }
}