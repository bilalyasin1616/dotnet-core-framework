using System.Net;

namespace Framework.Exceptions
{
    public class ConflictException : StatusCodeException
    {
        public ConflictException(string message, string type) : base(message, type, HttpStatusCode.Conflict)
        {
        }
    }
}