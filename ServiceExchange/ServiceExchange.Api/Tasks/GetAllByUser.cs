using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Microsoft.Identity.Web;
using Service.UseCases.Categories;
using Service.UseCases.Categories.GetAll;
using Service.UseCases.Tasks;
using Service.UseCases.Tasks.GetAllByUser;
using ServiceExchange.Api.Categories;

namespace ServiceExchange.Api.Tasks;

public class GetAll(IMediator _mediator) : EndpointWithoutRequest<GetTasksByUserResponse>
{
    public override void Configure()
    {
        Get("/user/tasks");
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var currentPrincipalId = HttpContext.User.GetObjectId();
        
        Result<List<TaskDTO>> result = await _mediator.Send(new GetAllTasksByUserQuery(currentPrincipalId), cancellationToken);

        if (result.IsSuccess)
        {
            Response = new GetTasksByUserResponse()
            {
                Tasks = result.Value.Select(c => new TaskRecord(c.Id, c.Title, c.Description)).ToList()
            };
        }
    }
}