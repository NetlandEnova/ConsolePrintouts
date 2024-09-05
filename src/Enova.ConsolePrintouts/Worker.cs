using Enova.ConsolePrintouts.Services;
using Soneta.Business;
using Soneta.Business.UI;
using Task = System.Threading.Tasks.Task;

namespace Enova.ConsolePrintouts;

public class Worker : BackgroundService
{
    private const string PRINT_PATH = "C:\\wydruk.pdf";
    private const string TEMPLATE =
        "C:\\Program Files (x86)\\Soneta\\enova365 2406.1.3\\Aspx\\kadry\\historia wyksztalcenia.aspx";

    private readonly IEnovaService task;

    public Worker(IEnovaService task)
    {
        this.task = task;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var context = Context.Empty.Clone(task.GetSession());
        print(context);

        await Task.CompletedTask;
    }

    private static byte[] print(Context cx)
    {
        byte[] buf = Array.Empty<byte>();

        ReportResult rr = new()
        {
            TemplateFileSource = AspxSource.Local,
            Context = cx,
            TemplateFileName = getAspxFile(),
            Format = ReportResultFormat.PDF,
        };

        IReportService report = cx.Session.GetRequiredService<IReportService>();

        Exception? exception = null;
        try
        {
            using Stream stream = report.GenerateReport(rr);

            buf = new byte[stream.Length];
            stream.Read(buf, 0, buf.Length);
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        File.Delete(rr.TemplateFileName!);

        if (buf.Length == 0)
        {
            throw new($"Błąd generowania wydruku: '{exception!.Message}'", exception);
        }

        using FileStream fileStream = new(PRINT_PATH, FileMode.Create, FileAccess.Write);
        fileStream.Write(buf, 0, buf.Length);

        return buf;
    }

    private static string? getAspxFile()
    {
        try
        {
            string tempFilePath = Path.GetTempFileName();
            string fileContent = File.ReadAllText(TEMPLATE);

            File.WriteAllText(tempFilePath, fileContent);

            return tempFilePath;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił problem z utworzeniem pliku tymczasowego {ex.Message}");
            return null;
        }
    }
}
