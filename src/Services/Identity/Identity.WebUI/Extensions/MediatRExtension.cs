using Identity.Application.Behaviours;
using MediatR.Pipeline;

namespace Identity.WebUI.Extensions;

public static class MediatRExtension
{
    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
        services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(LoginUserHandler)));
    }
}
