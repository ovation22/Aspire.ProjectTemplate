using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.ProjectTemplate.ApiService.Handlers;

internal sealed class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var errorId = Guid.NewGuid().ToString();
        var (statusCode, problemDetails, logLevel) = exception switch
        {
            KeyNotFoundException => (
                StatusCodes.Status404NotFound,
                new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Not Found",
                    Detail = exception.Message,
                    Extensions = { ["errorId"] = errorId }
                },
                LogLevel.Warning),
            OperationCanceledException => (
                StatusCodes.Status400BadRequest,
                new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Request Canceled",
                    Detail = exception.Message,
                    Extensions = { ["errorId"] = errorId }
                },
                LogLevel.Information),
            _ => (
                StatusCodes.Status500InternalServerError,
                new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred.",
                    Extensions = { ["errorId"] = errorId }
                },
                LogLevel.Error)
        };

        logger.Log(logLevel, exception, "Error Id: {ErrorId}, Message: {ExceptionMessage}", errorId, exception.Message);

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

        return true;
    }
}
