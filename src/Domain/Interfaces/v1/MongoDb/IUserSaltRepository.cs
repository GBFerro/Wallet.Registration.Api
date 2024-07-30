using Wallet.Registration.Domain.Entities.v1.MongoDB.UserSalt;

namespace Wallet.Registration.Domain.Interfaces.v1.MongoDb
{
    public interface IUserSaltRepository
    {
        Task SaveUserSaltAsync(UserSaltMongoEntity user);
        Task<UserSaltMongoEntity> RetrieveUserSaltAsync(string govId);
    }
}
