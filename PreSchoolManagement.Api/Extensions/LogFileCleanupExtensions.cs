using Serilog;

namespace SchoolAdmission.Api.Extensions;

public static class LogFileCleanupExtensions
{
    public static string EnsureLogDirectory(string contentRootPath)
    {
        var logDirectory = Path.Combine(contentRootPath, "Logs");

        if (!Directory.Exists(logDirectory))
            Directory.CreateDirectory(logDirectory);

        return logDirectory;
    }

    public static void DeleteOldLogFiles(string logDirectory, TimeSpan retentionPeriod)
    {
        if (!Directory.Exists(logDirectory))
            return;

        var cutoffTime = DateTime.UtcNow.Subtract(retentionPeriod);

        foreach (var file in Directory.EnumerateFiles(logDirectory, "*.log", SearchOption.TopDirectoryOnly))
        {
            if (File.GetLastWriteTimeUtc(file) < cutoffTime)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    Log.Warning(ex, "Failed to delete old log file: {FilePath}", file);
                }
            }
        }
    }
}
