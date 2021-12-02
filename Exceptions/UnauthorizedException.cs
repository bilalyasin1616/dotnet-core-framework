using System.Net;

namespace Framework.Exceptions
{
    public class UnauthorizedException : StatusCodeException
    {
        public UnauthorizedException(string message, string type) : base(message, type, HttpStatusCode.Unauthorized)
        {
        }
    }
}