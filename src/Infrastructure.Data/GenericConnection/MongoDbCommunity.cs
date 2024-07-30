using MongoDB.Driver;
using System.Net;
using Wallet.Registration.CrossCutting.Configuration;
using Wallet.Registration.CrossCutting.Configuration.Information;
using Wallet.Registration.CrossCutting.Exceptions;

namespace Wallet.Registration.Infrastructure.Data.GenericConnection
{
    public class MongoDbCommunity<TEntity> where TEntity : class
    {
        protected readonly IMongoDatabase _database;
        private readonly string _collection;

        public MongoDbCommunity(
            string connectionId,
            string databaseName,
            string collection,
            AppSettings appSettings
        )
        {
            try
            {
                _collection = collection;

                string connectionString = appSettings.MongoDbSettings.ConnectionString;

                MongoClient client = new MongoClient(connectionString);
                _database = client.GetDatabase(databaseName);
            }
            catch (Exception)
            {
                MongoDbInformation mongoDbInformation = new MongoDbInformation() { Method = "CreateConnection", Collection = collection, Error = "ConnectionFailed" };

                throw new MongoDbException<MongoDbInformation>(mongoDbInformation, HttpStatusCode.ServiceUnavailable);
            }
        }

        public FilterDefinitionBuilder<T> CreateFilterBuilder<T>() =>
            Builders<T>.Filter;

    }
}
