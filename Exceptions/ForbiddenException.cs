using System.Net;

namespace Framework.Exceptions
{
    public class ForbiddenException : StatusCodeException
    {
        public ForbiddenException(string message, string type) : base(message, type, HttpStatusCode.Forbidden)
        {
        }
    }
}