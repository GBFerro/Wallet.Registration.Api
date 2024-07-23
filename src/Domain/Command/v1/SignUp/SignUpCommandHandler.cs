using MediatR;
using Wallet.Registration.Domain.Entities.v1.MongoDB.SignUp;
using Wallet.Registration.Domain.Interfaces.v1.MongoDb;

namespace Wallet.Registration.Domain.Command.v1.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignUpCommandResponse>
    {
        private readonly IMongoDbRepository _mongoDB;

        public SignUpCommandHandler(
            IMongoDbRepository mongoDB
        )
        {
            _mongoDB = mongoDB;
        }

        public async Task<SignUpCommandResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User newUser = new User()
                {
                    Username = request.User,
                    Email = request.Email,
                    Phone = request.Phone
                };
                await _mongoDB.RegisterUser(newUser);

                return new SignUpCommandResponse() { User = newUser.Username };
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
