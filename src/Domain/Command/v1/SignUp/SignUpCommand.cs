using MediatR;
using Wallet.Registration.Domain.Command.v1.SignUp.Models.Request;

namespace Wallet.Registration.Domain.Command.v1.SignUp
{
    public record SignUpCommand : IRequest<SignUpCommandResponse>
    {
        public User User { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
