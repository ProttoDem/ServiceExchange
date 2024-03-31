using FastEndpoints;
using MediatR;
using Service.UseCases.Categories.Create;

namespace ServiceExchange.Categories;

public class Create(IMediator _mediator)
    : Endpoint<CreateCategoryRequest, CreateCategoryResponse>
{
    public override void Configure()
    {
        Post(CreateCategoryRequest.Route);
        Policies("AuthZPolicy");
        Summary(s =>
        {
            // XML Docs are used by default but are overridden by these properties:
            //s.Summary = "Create a new Contributor.";
            //s.Description = "Create a new Contributor. A valid name is required.";
            s.ExampleRequest = new CreateCategoryRequest() { Title = "Category Title", Description = "Category Description"};
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
            Response = new CreateCategoryResponse(result.Value, request.Title!, request.Description);
            return;
        }
    }
}