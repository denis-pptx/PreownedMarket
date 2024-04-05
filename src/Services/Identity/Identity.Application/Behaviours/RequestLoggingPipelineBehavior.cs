namespace Identity.Application.Behaviours;

public class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("Processing request {RequestName}: {@Request}", 
            requestName, request);

        try
        {
            TResponse result = await next();
            _logger.LogInformation("Completed request {RequestName} successfully", requestName);

            return result;
        }
        catch (Exception ex)
        {
            using (LogContext.PushProperty("Error", ex, destructureObjects: true))
            _logger.LogError("Completed request {RequestName} with error", requestName);

            throw;
        }
    }
}