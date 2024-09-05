namespace Enova.ConsolePrintouts.Extensions;

public static class EnovaDependencyInjectionExtensions
{
    public static IServiceCollection AddEnovaServices(this IServiceCollection services)
    {
        return services.AddTransient(provider => provider.GetRequiredService<EnovaStarter>().CreateSession());
    }
}
