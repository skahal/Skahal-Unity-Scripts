using System.Reflection;
using System.Text;
using Skahal.Logging;

public static class ReflectionTypeLoadExceptionExtensions
{
    public static void Log(this ReflectionTypeLoadException ex, ISHLogStrategy log, string message)
    {
        var builder = new StringBuilder();
        builder.AppendLine(message);
        builder.AppendLine("ReflectionTypeLoadException: {0}".With(ex.Message));
        builder.AppendLine("    LoaderExceptions:".With(ex.Message));

        foreach (var l in ex.LoaderExceptions)
        {
            builder.AppendLine("        * {0}: {1}".With(l.GetType().Name, l.Message));
        }

        log.Error(builder.ToString());
    }

    public static void Log(this ReflectionTypeLoadException ex, string message)
    {
        Log(ex, SHLog.LogStrategy, message);
    }
}
