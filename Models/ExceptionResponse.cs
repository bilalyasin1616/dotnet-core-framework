using System;

namespace Framework.Models
{
    public class ExceptionResponse
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public ExceptionResponse(Exception exception)
        {
            Message = exception.Message;
            StackTrace = exception.StackTrace;
        }
    }
}
