using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wallet.Registration.Domain.Command.v1.SignUp;

namespace Wallet.Registration.Api.Controller.Query;

public class RegisrationQuery
{
    [ProducesResponseType(
        (int)HttpStatusCode.OK
    )]
    public async Task<int> RetrieveUser(
        [Service] IMediator mediator,
        SignUpCommand command,
        CancellationToken cancellationToken
    ) => (int)HttpStatusCode.OK;
    // await mediator.Send(command, cancellationToken);
}
