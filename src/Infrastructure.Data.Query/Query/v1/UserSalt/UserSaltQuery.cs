using MediatR;

namespace Wallet.Registration.Infrastructure.Data.Query.Query.v1.UserSalt
{
    public class UserSaltQuery : IRequest<UserSaltQueryResponse>
    {
        public string GovId { get; set; }
    }
}
