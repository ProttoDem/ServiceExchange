using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Service.UseCases.Categories.Get;

namespace ServiceExchange.Api.Categories;

public class GetById(IMediator _mediator)
    : Endpoint<GetCategoryByIdRequest, CategoryRecord>
{
    public override void Configure()
    {
        Get(GetCategoryByIdRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetCategoryByIdRequest request,
        CancellationToken cancellationToken)
    {
        var command = new GetCategoryQuery(request.CategoryId);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            Response = new CategoryRecord(result.Value.Id, result.Value.Title, result.Value.Description);
        }
    }
}