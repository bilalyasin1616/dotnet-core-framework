using System.Net;

namespace Framework.Exceptions
{
    public class BadRequestException : StatusCodeException
    {
        public BadRequestException(string message, string type) : base(message, type, HttpStatusCode.BadRequest)
        {
        }
    }
}