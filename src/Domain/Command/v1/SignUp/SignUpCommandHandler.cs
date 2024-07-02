using MediatR;

namespace Wallet.Registration.Domain.Command.v1.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignUpCommandResponse>
    {
        public async Task<SignUpCommandResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            return new SignUpCommandResponse();
        }
    }
}
