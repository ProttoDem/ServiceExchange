using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Service.UseCases.Tasks;
using Service.UseCases.Tasks.Create;

namespace ServiceExchange.Api.Tasks;

public class Create(IMediator _mediator)
    : Endpoint<CreateTaskRequest, Results<BadRequest, Ok<CreateTaskResponse>>>
{
    public override void Configure()
    {
        Post(CreateTaskRequest.Route);
        Version(1);
    }

    public override async Task<Results<BadRequest, Ok<CreateTaskResponse>>> HandleAsync(CreateTaskRequest request,CancellationToken cancellationToken)
    {
        var currentPrincipalId = HttpContext.User.GetObjectId();

        if (currentPrincipalId.IsNullOrEmpty())
        {
            return TypedResults.BadRequest();
        }
        
        Result<TaskDTO> result = await _mediator.Send(new CreateTaskCommand
            (currentPrincipalId, request.Title, request.Description, request.StartTime, 
                request.FinishTime, request.Price, request.CategoryId, request.IsRepeatable), cancellationToken);

        if (result.IsSuccess)
        {
            return TypedResults.Ok(new CreateTaskResponse
            {
                Task = new TaskRecord(result.Value.Id, result.Value.Title, result.Value.Description,
                    result.Value.Calendar.StartTime)
            });
        }

        return TypedResults.BadRequest();
    }
}