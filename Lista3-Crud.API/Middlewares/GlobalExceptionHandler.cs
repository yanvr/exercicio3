using Lista3_Crud.Domain;
using Lista3_Crud.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Lista3_Crud.API.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails();

            if (exception is DomainException)
            {
                problemDetails = new ProblemDetails
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = exception.Message
                };
            }
            else
            {
                problemDetails = new ProblemDetails()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "A server error occurred"
                };
            }

            httpContext.Response.StatusCode = problemDetails.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}