using Identity.Application.Behaviours;
using MediatR.Pipeline;

namespace Identity.WebUI.Extensions;

public static class MediatRExtension
{
    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblyContaining(typeof(LoginUserHandler));

            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            cfg.AddBehavior(typeof(IRequestPreProcessor<>), typeof(LoggingPreProcessorBehaviour<>));

            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        });
    }
}
