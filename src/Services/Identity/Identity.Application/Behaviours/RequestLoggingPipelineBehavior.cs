using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Identity.Application.Behaviours;

public class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Processing request {RequestName}: {@Request}", 
            requestName, request);

        try
        {
            var result = await next();
            logger.LogInformation("Completed request {RequestName} successfully", requestName);

            return result;
        }
        catch (Exception ex)
        {
            using (LogContext.PushProperty("Error", ex, destructureObjects: true))
                logger.LogError("Completed request {RequestName} with error", requestName);

            throw;
        }
    }
}