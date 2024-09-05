namespace Enova.ConsolePrintouts.Options;

public class WorkerOptions
{
    public static string SectionName { get; } = "Worker";

    public bool ExecuteOnStartup { get; set; }
    public TimeSpan ExecutionTime { get; set; }
}
