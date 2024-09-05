using Soneta.Business;
using Soneta.Business.App;
using Soneta.Start;

namespace Enova.ConsolePrintouts;

internal sealed class EnovaStarter : IDisposable
{
    private readonly IConfiguration configuration;

    private Database? db;
    private Login? login;

    public EnovaStarter(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public Database Db => db ?? throw new InvalidOperationException("Enova not initialized");
    internal Login Login => login ?? throw new InvalidOperationException("Enova not initialized");

    public void InitializeAndLogIn()
    {
        loadEnovaAssemblies();
        initializeDb(configuration["Enova:Database"]!);
        logIn(configuration["Enova:Login"]!, configuration["Enova:Password"]!);
    }

    public Session CreateSession()
    {
        if (login is null)
        {
            throw new InvalidOperationException("Enova not initialized");
        }

        return Login.CreateSession(false, false);
    }

    public void Dispose()
    {
        login?.Database?.Dispose();
        login?.Dispose();
        db?.Dispose();
    }

    private void loadEnovaAssemblies()
    {
        Loader loader = new()
        {
            WithUI = true,
            WithNet = false,
            WithExtensions = true,
        };

        loader.Load();
    }

    private void initializeDb(string dbname)
    {
        db = BusApplication.Instance[dbname];
    }

    private void logIn(string user, string pwd)
    {
        if (login is not null)
        {
            return;
        }

        login = Db.Login(false, user, pwd);
    }
}
