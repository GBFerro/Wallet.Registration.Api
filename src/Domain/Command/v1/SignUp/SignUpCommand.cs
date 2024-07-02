using MediatR;

namespace Wallet.Registration.Domain.Command.v1.SignUp
{
    public class SignUpCommand : IRequest<SignUpCommandResponse>
    {
        public string User { get; set; }
        public string Email { get; set; }
    }
}
