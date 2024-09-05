using Soneta.Business;

namespace Enova.ConsolePrintouts.Services;

public class EnovaService : IEnovaService
{
    private readonly Session session;

    public EnovaService(Session session)
    {
        this.session = session;
    }

    public Session GetSession() => session;
}
