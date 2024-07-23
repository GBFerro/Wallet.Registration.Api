using System.Net;
using Wallet.Registration.CrossCutting.Configuration;
using Wallet.Registration.CrossCutting.Configuration.Logs;
using Wallet.Registration.CrossCutting.Exceptions;
using Wallet.Registration.Domain.Entities.v1.MongoDB.SignUp;
using Wallet.Registration.Domain.Interfaces.v1.MongoDb;
using Wallet.Registration.Infrastructure.Data.GenericConnection;

namespace Wallet.Registration.Infrastructure.Data.Repository
{
    public class MongoDbRepository : MongoDbCommunity<MongoDbRepository>, IMongoDbRepository
    {
        private readonly string _collection;

        public MongoDbRepository(
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

        public async Task RegisterUser(User entity)
        {
            try
            {
                await _database
                    .GetCollection<User>(_collection)
                    .InsertOneAsync(entity);
            }
            catch (Exception)
            {
                MongoDbLog mongoDbLog = new MongoDbLog()
                {
                    Method = "AddAsync",
                    Collection = _collection,
                    Error = "ConnectionFailed"
                };

                throw new MongoDbException<MongoDbLog>(
                    mongoDbLog,
                    HttpStatusCode.BadRequest
                );
            }
        }
    }
}
