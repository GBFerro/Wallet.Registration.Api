using MongoDB.Driver;
using System.Net;
using Wallet.Registration.CrossCutting.Configuration;
using Wallet.Registration.CrossCutting.Configuration.Information;
using Wallet.Registration.CrossCutting.Exceptions;
using Wallet.Registration.Domain.Entities.v1.MongoDB.SignUp;
using Wallet.Registration.Domain.Interfaces.v1.MongoDb;
using Wallet.Registration.Infrastructure.Data.GenericConnection;

namespace Wallet.Registration.Infrastructure.Data.Repository
{
    public class UserRegistryRepository : MongoDbCommunity<UserRegistryRepository>, IUserRegistryRepository
    {
        private readonly string _collection;

        public UserRegistryRepository(
            string connectionId,
            string databaseName,
            string collection,
            AppSettings appSettings
        ) : base(
            connectionId,
            databaseName,
            collection,
            appSettings
        )
        {
            _collection = collection;
        }

        public async Task RegisterUserAsync(SignUpMongoEntity entity)
        {
            try
            {
                await _database
                    .GetCollection<SignUpMongoEntity>(_collection)
                    .InsertOneAsync(entity);
            }
            catch (Exception)
            {
                MongoDbInformation mongoDbLog = new MongoDbInformation()
                {
                    Method = "AddAsync",
                    Collection = _collection,
                    Error = "RegisterUserFailed"
                };

                throw new MongoDbException<MongoDbInformation>(
                    mongoDbLog,
                    HttpStatusCode.BadRequest
                );
            }
        }

        public async Task<SignUpMongoEntity> GetUserAsync(string govId)
        {
            try
            {
                var filterBuilder = CreateFilterBuilder<SignUpMongoEntity>();

                var filter = filterBuilder.Eq(user => user.User.GovId, govId);

                var response = await _database
                    .GetCollection<SignUpMongoEntity>(_collection)
                    .Find(filter).Limit(1).SingleOrDefaultAsync();

                return response;
            }
            catch (Exception)
            {
                MongoDbInformation mongoDbLog = new MongoDbInformation()
                {
                    Method = "AddAsync",
                    Collection = _collection,
                    Error = "RegisterUserFailed"
                };

                throw new MongoDbException<MongoDbInformation>(
                    mongoDbLog,
                    HttpStatusCode.BadRequest
                );
            }
        }
    }
}
