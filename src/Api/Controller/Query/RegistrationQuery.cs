using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wallet.Registration.Infrastructure.Data.Query.Query.v1.UserSalt;

namespace Wallet.Registration.Api.Controller.Query;

public class RegistrationQuery
{
    [ProducesResponseType(
        typeof(UserSaltQueryResponse),
        (int)HttpStatusCode.OK
    )]
    public async Task<UserSaltQueryResponse> RetrieveUserSalt(
        [Service] IMediator mediator,
        UserSaltQuery query,
        CancellationToken cancellationToken
    ) =>
        await mediator.Send(query, cancellationToken);
}
