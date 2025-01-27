using Blogs.Contracts.Responses;
using FluentValidation;

namespace Blogs.API.Mapping
{
    public class ValidationMappingMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 400;
                var validationFailureResponse = new ValidationFailureResponse
                {
                    Errors = ex.Errors.Select(s => new ValidationResponse
                    {
                        PropertyName = s.PropertyName,
                        Message = s.ErrorMessage
                    })
                };

                await context.Response.WriteAsJsonAsync(validationFailureResponse);
            }
        }
    }
}
