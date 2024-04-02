using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Identity.Application.Behaviours;

public class LoggingBehaviour<TRequest>(ILogger<TRequest> _logger, ICurrentUserService _currentUserService) 
    : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        var user = await _currentUserService.GetCurrentUserAsync();

        _logger.LogInformation("Request: {@RequestName} {@UserId} {@UserName} {@Request}",
            requestName, user?.Id, user?.UserName, request);
    }
}