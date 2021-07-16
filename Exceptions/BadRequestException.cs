using System.Collections.Generic;

namespace Framework.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string msg, List<string> errors = null) : base(msg)
        {
            Errors = errors;
        }
    }
}
