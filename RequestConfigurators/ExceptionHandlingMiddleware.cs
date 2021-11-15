using Framework.Exceptions;
using Framework.Models;
using Microsoft.AspNetCore.Http;
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadRequestException badRequestException)
            {
                await HandleBadRequestExceptionAsync(context, badRequestException);
            }
            catch (CustomException customException)
            {
                await HandleCustomExceptionAsync(context, customException);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleBadRequestExceptionAsync(HttpContext context, BadRequestException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var exceptionsResponse = new ExceptionResponse(exception, exception.Errors)
            {
                Message = "Bad Request to the server."
            };
            return context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(exceptionsResponse));
        }

        private static Task HandleCustomExceptionAsync(HttpContext context, CustomException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var exceptionsResponse = new ExceptionResponse(exception, exception.Errors);
            return context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(exceptionsResponse));
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var exceptionsResponse = new ExceptionResponse(exception)
            {
                Message = "Internal server error, please contact support."
            };
            return context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(exceptionsResponse));
        }
    }
}
