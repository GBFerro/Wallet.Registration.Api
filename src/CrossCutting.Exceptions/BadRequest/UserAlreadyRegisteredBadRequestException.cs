using System.Net;

namespace Wallet.Registration.CrossCutting.Exceptions.BadRequest
{
    public class UserAlreadyRegisteredBadRequestException : GenericException
    {
        public UserAlreadyRegisteredBadRequestException(HttpStatusCode statusCode)
            : base(statusCode)
        {
        }

        public UserAlreadyRegisteredBadRequestException(string message, HttpStatusCode statusCode)
            : base(message, statusCode)
        {
        }

        public UserAlreadyRegisteredBadRequestException(string message, Exception innerException, HttpStatusCode statusCode)
            : base(message, innerException, statusCode)
        {
        }
    }
}
