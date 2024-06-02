using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Microsoft.Identity.Web;
using Service.UseCases.Tasks;
using Service.UseCases.Tasks.GetAllActiveOpenTasks;
using Service.UseCases.Tasks.GetAllByUser;

namespace ServiceExchange.Api.Tasks;

public class GetAllActiveOpen(IMediator _mediator) : EndpointWithoutRequest<GetTasksActiveOpenResponse>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("/tasks");
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        Result<List<TaskDTO>> result = await _mediator.Send(new GetAllActiveOpenTasksQuery(), cancellationToken);

        if (result.IsSuccess)
        {
            Response = new GetTasksActiveOpenResponse()
            {
                Tasks = result.Value.Select(c => new TaskRecord(c.Id, c.Title, c.Description, c.Calendar.StartTime)).ToList()
            };
        }
    }
}