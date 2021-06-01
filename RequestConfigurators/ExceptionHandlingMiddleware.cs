using Framework.Exceptions;
using Framework.Helper;
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
            return context.Response.WriteAsync(ResponseBase.GetBadRequest(exception.Message, exception.Errors).ToString());
        }

        private static Task HandleCustomExceptionAsync(HttpContext context, CustomException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = EnvironmentHelper.IsDevelopment() || EnvironmentHelper.IsStaging() ?
                ResponseBase.GetInternalServerError(exception.Message, exception, exception.Errors, exception.Warnings) :
                ResponseBase.GetInternalServerError(exception.Message, null, exception.Errors, exception.Warnings);
            return context.Response.WriteAsync(response.ToString());
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = EnvironmentHelper.IsDevelopment() || EnvironmentHelper.IsStaging() ?
                ResponseBase.GetInternalServerError("Internal server error, please contact support.", exception) : ResponseBase.GetInternalServerError(exception.Message);
            return context.Response.WriteAsync(response.ToString());
        }
    }
}
