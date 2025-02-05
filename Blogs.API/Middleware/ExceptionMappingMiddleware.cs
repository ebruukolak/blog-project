using Blogs.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Blogs.API.Middleware
{
    public class ExceptionMappingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string message = "An unexpected error occurred.";

            switch (exception)
            {
                case UserAlreadyExistsException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
                case InvalidCredentialsException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    message = exception.Message;
                    break;
                case AppException appEx:
                    statusCode = appEx.StatusCode;
                    message = appEx.Message;
                    break;
                default:
                    break;
            }

            var response = new { error = message };
            var json = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(json);
        }
    }

}
