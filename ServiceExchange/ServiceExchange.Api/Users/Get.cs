using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Service.UseCases.Tasks;
using Service.UseCases.Tasks.GetAllByUser;
using Service.UseCases.Users;
using Service.UseCases.Users.Create;
using Service.UseCases.Users.Get;
using ServiceExchange.Api.Tasks;

namespace ServiceExchange.Api.Users;

public class Get(IMediator _mediator) : EndpointWithoutRequest<GetUserResponse>
{
    public override void Configure()
    {
        AllowAnonymous();
        Version(1);
        Get("/user");
    }

    public override async Task<Results<Ok, BadRequest, NotFound>> HandleAsync(CancellationToken cancellationToken)
    {
        var currentPrincipalId = HttpContext.User.GetObjectId();

        if (currentPrincipalId.IsNullOrEmpty())
        {
            return TypedResults.BadRequest();
        }
        
        Result<UserDTO> getResult = await _mediator.Send(new GetUserBySystemIdQuery(currentPrincipalId), cancellationToken);

        if (getResult.IsSuccess)
        {
            return TypedResults.Ok();
        }
        
        if (getResult.Status == ResultStatus.NotFound)
        {
            Result<Guid> createResult = await _mediator.Send(new CreateUserBySystemIdCommand(currentPrincipalId), cancellationToken);

            if (createResult.IsSuccess)
            {
                return TypedResults.Ok();
            }
        }

        return TypedResults.NotFound();
    }
}