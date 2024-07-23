using System.Net;

namespace Wallet.Registration.CrossCutting.Exceptions
{
    public class MongoDbException<T> : Exception where T : class
    {
        public HttpStatusCode StatusCode { get; }
        public T MongoLog { get; set; }

        public MongoDbException(
            T error,
            HttpStatusCode statusCode
        )
        {
            MongoLog = error;
            StatusCode = statusCode;
        }

        public MongoDbException(
            T error,
            string message,
            HttpStatusCode statusCode
        ) : base(
            message
        )
        {
            MongoLog = error;
            StatusCode = statusCode;
        }

        public MongoDbException(
            string message,
            HttpStatusCode statusCode
        ) : base(
            message
        )
        {
            StatusCode = statusCode;
        }

        public MongoDbException(
            T error,
            string message,
            Exception innerException,
            HttpStatusCode statusCode
        ) : base(
            message,
            innerException
        )
        {
            MongoLog = error;
            StatusCode = statusCode;
        }
    }
}
