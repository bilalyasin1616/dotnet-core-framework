using Framework.Helpers;
using System;
using System.Collections.Generic;

namespace Framework.Models
{
    public class ExceptionResponse
    {
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public List<string> Errors { get; set; }

        public ExceptionResponse(Exception exception, List<string> errors=null)
        {
            if (!EnvironmentHelper.IsProduction())
            {
                ExceptionMessage = exception.Message;
                InnerExceptionMessage = exception.InnerException?.Message;
                StackTrace = exception.StackTrace;
                Errors = errors;
                return;
            }
            ExceptionMessage = "Exception occured, check logs for more details.";
            Errors = errors;
        }
    }
}
