using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Service.UseCases.Categories.Get;
using Service.UseCases.Categories.Update;

namespace ServiceExchange.Api.Categories;

/// <summary>
/// Update an existing Contributor.
/// </summary>
/// <remarks>
/// Update an existing Contributor by providing a fully defined replacement set of values.
/// See: https://stackoverflow.com/questions/60761955/rest-update-best-practice-put-collection-id-without-id-in-body-vs-put-collecti
/// </remarks>
public class Update(IMediator _mediator)
    : Endpoint<UpdateCategoryRequest, UpdateCategoryResponse>
{
    public override void Configure()
    {
        Put(UpdateCategoryRequest.Route);
        Version(1);
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        UpdateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateCategoryCommand(request.CategoryId, request.Title!, request.Description!), cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        var query = new GetCategoryQuery(request.CategoryId);

        var queryResult = await _mediator.Send(query, cancellationToken);

        if (queryResult.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (queryResult.IsSuccess)
        {
            var dto = queryResult.Value;
            Response = new UpdateCategoryResponse(new CategoryRecord(dto.Id, dto.Title, dto.Description));
            return;
        }
    }
}