using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Service.UseCases.Categories;
using Service.UseCases.Categories.GetAll;

namespace ServiceExchange.Categories;

public class GetAll(IMediator _mediator) : EndpointWithoutRequest<GetAllCategoriesResponse>
{
    public override void Configure()
    {
        Get("/Categories");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        Result<List<CategoryDTO>> result = await _mediator.Send(new GetAllCategoriesQuery(null, null), cancellationToken);

        if (result.IsSuccess)
        {
            Response = new GetAllCategoriesResponse()
            {
                Categories = result.Value.Select(c => new CategoryRecord(c.Id, c.Title, c.Description)).ToList()
            };
        }
    }
}