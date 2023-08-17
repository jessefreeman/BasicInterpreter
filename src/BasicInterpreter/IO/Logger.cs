namespace JesseFreeman.BasicInterpreter.IO;

public enum LogTag
{
    All, // Catch-all tag
    Execution,
    CommandIndex,
    Jumping,
    ParseTree,
    // Add more tags as needed
}

public static class Logger
{
    
    public static HashSet<LogTag> EnabledTags { get; } = new() { LogTag.All }; // By default, enable the catch-all tag

    public static IOutputWriter OutputWriter { get; set; } // Set this to your implementation of IOutputWriter

    public static void Log(string message, LogTag tag = LogTag.All)
    {
        if ((EnabledTags.Contains(LogTag.All) || EnabledTags.Contains(tag)) && OutputWriter != null)
        {
            OutputWriter.WriteLine(message);
        }
    }

    public static void EnableTag(LogTag tag)
    {
        EnabledTags.Add(tag);
    }

    public static void DisableTag(LogTag tag)
    {
        EnabledTags.Remove(tag);
    }
}
