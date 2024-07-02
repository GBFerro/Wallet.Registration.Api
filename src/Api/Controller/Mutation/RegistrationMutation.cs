using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wallet.Registration.Domain.Command.v1.SignUp;

namespace Wallet.Registration.Api.Controller.Mutation;

public class RegistrationMutation
{
    [ProducesResponseType(
        typeof(SignUpCommandResponse),
        (int)HttpStatusCode.Created
    )]
    public async Task<SignUpCommandResponse> Authenticate(
    [Service] IMediator mediator,
    SignUpCommand command,
    CancellationToken cancellationToken
) =>
    await mediator.Send(command, cancellationToken);
}
