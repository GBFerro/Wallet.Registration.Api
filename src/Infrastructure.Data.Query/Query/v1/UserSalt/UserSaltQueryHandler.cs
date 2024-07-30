using MediatR;
using Wallet.Registration.Domain.Entities.v1.MongoDB.UserSalt;
using Wallet.Registration.Domain.Interfaces.v1.MongoDb;

namespace Wallet.Registration.Infrastructure.Data.Query.Query.v1.UserSalt
{
    public class UserSaltQueryHandler : IRequestHandler<UserSaltQuery, UserSaltQueryResponse>
    {
        private readonly IUserSaltRepository _userSaltRepository;

        public UserSaltQueryHandler(
            IUserSaltRepository userSaltRepository
        )
        {
            _userSaltRepository = userSaltRepository;
        }

        public async Task<UserSaltQueryResponse> Handle(UserSaltQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userSaltMongo = await _userSaltRepository.RetrieveUserSaltAsync(request.GovId);
                return MapperResponse(userSaltMongo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private UserSaltQueryResponse MapperResponse(UserSaltMongoEntity userSaltMongo) =>
            new UserSaltQueryResponse() { Salt = userSaltMongo.Salt };
    }
}
