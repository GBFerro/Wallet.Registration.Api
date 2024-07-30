using Wallet.Registration.Domain.Entities.v1.MongoDB.SignUp;

namespace Wallet.Registration.Domain.Interfaces.v1.MongoDb;

public interface IUserRegistryRepository
{
    Task RegisterUserAsync(SignUpMongoEntity user);

    Task<SignUpMongoEntity> GetUserAsync(string govId);
}
