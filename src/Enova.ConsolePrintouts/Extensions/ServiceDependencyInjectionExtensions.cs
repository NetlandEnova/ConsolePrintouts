using Enova.ConsolePrintouts.Options;
using Enova.ConsolePrintouts.Services;

namespace Enova.ConsolePrintouts.Extensions;

public static class ServiceDependencyInjectionExtensions
{
    public static IServiceCollection AddCoreServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<WorkerOptions>(configuration.GetSection(WorkerOptions.SectionName));
        services.AddTransient<IEnovaService, EnovaService>();
        return services;
    }
}
