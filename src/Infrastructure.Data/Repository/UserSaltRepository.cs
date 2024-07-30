using MongoDB.Driver;
using System.Net;
using Wallet.Registration.CrossCutting.Configuration;
using Wallet.Registration.CrossCutting.Configuration.Information;
using Wallet.Registration.CrossCutting.Exceptions;
using Wallet.Registration.Domain.Entities.v1.MongoDB.UserSalt;
using Wallet.Registration.Domain.Interfaces.v1.MongoDb;
using Wallet.Registration.Infrastructure.Data.GenericConnection;

namespace Wallet.Registration.Infrastructure.Data.Repository
{
    public class UserSaltRepository : MongoDbCommunity<UserSaltRepository>, IUserSaltRepository
    {
        private readonly string _collection;

        public UserSaltRepository(
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

        public async Task<UserSaltMongoEntity> RetrieveUserSaltAsync(string govId)
        {
            try
            {
                var filterBuilder = CreateFilterBuilder<UserSaltMongoEntity>();

                var filter = filterBuilder.Eq(user => user.GovId, govId);

                var response = await _database
                    .GetCollection<UserSaltMongoEntity>(_collection)
                    .Find(filter).Limit(1).SingleAsync();

                return response;
            }
            catch (Exception)
            {
                MongoDbInformation mongoDbinformation = GenerateMongoInformation("AddAsync", "SaveSaltFailed");

                throw new MongoDbException<MongoDbInformation>(
                    mongoDbinformation,
                    HttpStatusCode.BadRequest
                );
            }
        }

        public async Task SaveUserSaltAsync(UserSaltMongoEntity entity)
        {
            try
            {
                await _database
                    .GetCollection<UserSaltMongoEntity>(_collection)
                    .InsertOneAsync(entity);
            }
            catch (Exception)
            {
                MongoDbInformation mongoDbinformation = GenerateMongoInformation("AddAsync", "SaveSaltFailed");

                throw new MongoDbException<MongoDbInformation>(
                    mongoDbinformation,
                    HttpStatusCode.BadRequest
                );
            }
        }
        private MongoDbInformation GenerateMongoInformation(string method, string errorMessage) =>
            new MongoDbInformation()
            {
                Method = method,
                Collection = _collection,
                Error = errorMessage
            };
    }

}
