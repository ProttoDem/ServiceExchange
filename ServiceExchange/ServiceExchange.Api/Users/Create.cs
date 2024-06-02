using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Service.UseCases.Users;
using Service.UseCases.Users.Create;
using Service.UseCases.Users.Get;

namespace ServiceExchange.Api.Users;

public class Create(IMediator _mediator) : EndpointWithoutRequest<CreateUserResponse>
{
    public override void Configure()
    {
        Version(1);
        Post("/user");
    }

    public override async Task<Results<Created, BadRequest>> HandleAsync(CancellationToken cancellationToken)
    {
        var currentPrincipalId = HttpContext.User.GetObjectId();

        if (currentPrincipalId.IsNullOrEmpty())
        {
            return TypedResults.BadRequest();
        }
        
        Result<Guid> result = await _mediator.Send(new CreateUserBySystemIdCommand(currentPrincipalId), cancellationToken);

        if (result.IsSuccess)
        {
            return TypedResults.Created();
        }

        return TypedResults.BadRequest();
    }
}