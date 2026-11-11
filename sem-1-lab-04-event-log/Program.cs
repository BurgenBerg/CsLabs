using System.ComponentModel.DataAnnotations;

namespace sem_1_lab_04_event_log;

class LogEntry
{
    public DateTime Timestamp;
    public string Level;
    public string Category;
    public string Message;
}
class Program
{
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("event_server.log");
        Console.WriteLine(lines.Length);

        string line = lines[0];

        LogEntry entry = new LogEntry();
        entry.Timestamp = DateTime.Parse(line.Substring(0, 23));
        int firstOpenBracket = line.IndexOf('[');
        int firstCloseBracket = line.IndexOf(']', firstOpenBracket);
        int secondOpenBracket = line.IndexOf('[', firstCloseBracket);
        int secondCloseBracket = line.IndexOf(']', secondOpenBracket);

        entry.Level = line.Substring(firstOpenBracket + 1, firstCloseBracket - firstOpenBracket - 1);
        entry.Category = line.Substring(secondOpenBracket + 1, secondCloseBracket - secondOpenBracket - 1);
        entry.Message = line.Substring(secondCloseBracket + 2);

        string vremya = entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff");

        Console.WriteLine($"{vremya} | {entry.Level} | {entry.Category} | {entry.Message}\n");

        LogEntry[] entries = ParseLog(lines);
        Console.WriteLine(entries.Length);
        Console.WriteLine($"{entries[0].Timestamp} {entries[0].Level} {entries[0].Category} {entries[0].Message}");
        Console.WriteLine($"{entries[999].Timestamp} {entries[999].Level} {entries[999].Category} {entries[999].Message}");
    }

    public static LogEntry[] ParseLog(string[] lines)
    {
        LogEntry[] result = new LogEntry[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            LogEntry entry = new LogEntry();
            entry.Timestamp = DateTime.Parse(line.Substring(0, 23));
            int firstOpenBracket = line.IndexOf('[');
            int firstCloseBracket = line.IndexOf(']', firstOpenBracket);
            int secondOpenBracket = line.IndexOf('[', firstCloseBracket);
            int secondCloseBracket = line.IndexOf(']', secondOpenBracket);

            entry.Level = line.Substring(firstOpenBracket + 1, firstCloseBracket - firstOpenBracket - 1);
            entry.Category = line.Substring(secondOpenBracket + 1, secondCloseBracket - secondOpenBracket - 1);
            entry.Message = line.Substring(secondCloseBracket + 2);

            result[i] = entry;
        }
        return result;
    }

    public static FilterByLevel(LogEntry[] entries, string level)
    {
        return LogEntry[];
    }
}