using Enova.ConsolePrintouts;
using Enova.ConsolePrintouts.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options => options.ServiceName = "Example Enova Service")
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        EnovaStarter enovaStarter = new(configuration);
        enovaStarter.InitializeAndLogIn();

        services.AddSingleton(enovaStarter)
            .AddEnovaServices();
        services.AddCoreServices(configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
