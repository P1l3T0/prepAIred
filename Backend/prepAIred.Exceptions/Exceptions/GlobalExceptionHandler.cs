using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;

namespace prepAIred.Exceptions
{
    public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService,ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "An unhandled exception occurred.");

            httpContext.Response.StatusCode = exception switch
            {
                InvalidCredentialsException => StatusCodes.Status401Unauthorized,
                NoUserLoggedInException => StatusCodes.Status401Unauthorized,
                InvalidAccessTokenException => StatusCodes.Status401Unauthorized,
                InvalidRefreshTokenException => StatusCodes.Status401Unauthorized,
                UserAlreadyExistsException => StatusCodes.Status409Conflict,
                ResourceNotFoundException => StatusCodes.Status404NotFound,
                EmptyFieldsException => StatusCodes.Status400BadRequest,
                ApplicationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext()
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails()
                {
                    Title = exception.Message,
                    Status = httpContext.Response.StatusCode,
                    Type = exception.GetType().Name
                }
            });
        }
    }
}