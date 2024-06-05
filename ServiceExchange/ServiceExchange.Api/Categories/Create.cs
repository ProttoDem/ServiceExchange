using FastEndpoints;
using MediatR;
using Service.UseCases.Categories.Create;

namespace ServiceExchange.Api.Categories;

public class Create(IMediator _mediator)
    : Endpoint<CreateCategoryRequest, CreateCategoryResponse>
{
    public override void Configure()
    {
        Post(CreateCategoryRequest.Route);
        Version(1);
        Policies("AdminUser");
        Summary(s =>
        {
            s.ExampleRequest = new CreateCategoryRequest() 
                { Title = "Category Title", Description = "Category Description"};
        });
    }

    public override async Task HandleAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateCategoryCommand(request.Title!,
            request.Description), cancellationToken);

        if (result.IsSuccess)
        {
            Response = new CreateCategoryResponse
                (result.Value, request.Title!, request.Description);
            return;
        }
    }
}