using Framework.Exceptions;
using Framework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Framework.RequestConfigurators
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ExceptionHandler> logger)
        {
            try
            {
                await _next(context);
            }
            catch (StatusCodeException statusCodeException)
            {
                logger.LogError(statusCodeException, statusCodeException.Message);
                await HandleStatusCodeException(context, statusCodeException);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Internal server error.");
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleStatusCodeException(HttpContext context, StatusCodeException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.HttpStatusCode;
            var exceptionsResponse = new ExceptionResponse()
            {
                Message = exception.Message,
                Type = exception.Type
            };
            return context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(exceptionsResponse));
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var exceptionsResponse = new ExceptionResponse()
            {
                Message = "Internal server error.",
                Type = "Unkown"
            };
            return context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(exceptionsResponse));
        }
    }
}