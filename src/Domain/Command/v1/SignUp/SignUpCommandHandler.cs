using MediatR;
using Wallet.Registration.CrossCutting.Configuration;

namespace Wallet.Registration.Domain.Command.v1.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignUpCommandResponse>
    {
        private readonly AppSettings _appSettings;

        public SignUpCommandHandler(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<SignUpCommandResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {

            var teste = _appSettings.MongoDbSettings;
            return new SignUpCommandResponse() { User = "giovani" };
        }
    }
}
