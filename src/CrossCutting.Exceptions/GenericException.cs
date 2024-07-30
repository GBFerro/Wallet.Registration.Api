using System.Net;

namespace Wallet.Registration.CrossCutting.Exceptions
{
    public class GenericException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public GenericException(
            HttpStatusCode statusCode
        )
        {
            StatusCode = statusCode;
        }

        public GenericException(
            string message,
            HttpStatusCode statusCode
        ) : base(
            message
        )
        {
            StatusCode = statusCode;
        }

        public GenericException(
            string message,
            Exception innerException,
            HttpStatusCode statusCode
        ) : base(
            message,
            innerException
        )
        {
            StatusCode = statusCode;
        }
    }
}
