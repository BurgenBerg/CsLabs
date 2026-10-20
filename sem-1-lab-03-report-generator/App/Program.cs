using System.Globalization;
using System.IO;
public class Program
{
    public static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("event_server.log");
        Console.WriteLine(BuildReport(lines));
    }
    public static string BuildReport(string[] lines)
    {
        DateTime date = FindEventDate(lines);
        string winner = FindWinner(lines);
        int winnerPoints = FindWinnerPoints(lines, winner);
        string eventItem = FindEventItem(lines, winner);
        string loserClan = FindLoserClan(lines);
        int loserPoints = FindLoserPoints(lines);
        int warnings = CountLevel(lines, "Warning");
        int errors = CountLevel(lines, "Error");
        string topPlayer = FindTopPlayer(lines);

        return $"Итоги события: Восстание Ледяного Пламени\n" +
               $"Дата: {date:dd.MM.yyyy}\n" +
               $"Победитель: {winner}\n" +
               $"Очки победителя: {winnerPoints}\n" +
               $"Ивентовый предмет: {eventItem}\n" +
               $"Утешительная награда {loserClan}: {loserPoints} очков\n" +
               $"Предупреждений во время события: {warnings}\n" +
               $"Ошибок во время события: {errors}\n" +
               $"Лучший игрок: {topPlayer}";
    }

    private static DateTime FindEventDate(string[] lines)
    {
        foreach (string line in lines)
        {
            if (line.Contains("Событие началось: Восстание Ледяного Пламени"))
                return DateTime.Parse(line.Substring(0, 10));
        }
        return DateTime.MinValue;
    }

    private static string FindWinner(string[] lines)
    {
        foreach (string line in lines)
        {
            if (line.Contains("объявлены победителями события"))
            {
                string message = line.Substring(line.IndexOf("] ") + 2);
                string[] parts = message.Split(' ');
                return parts[0] + " " + parts[1];
            }
        }
        return string.Empty;
    }

    private static int FindWinnerPoints(string[] lines, string winner)
    {
        string prefix = winner + " получили ";
        foreach (string line in lines)
        {
            if (line.Contains(prefix) && line.Contains(" очков события"))
            {
                string tail = line.Substring(line.IndexOf(prefix) + prefix.Length);
                string number = tail.Split(' ')[0];
                return int.Parse(number);
            }
        }
        return 0;
    }

    private static string FindEventItem(string[] lines, string winner)
    {
        string prefix = winner + " получили ивентовый предмет: ";
        foreach (string line in lines)
        {
            if (line.Contains(prefix))
            {
                return line.Substring(line.IndexOf(prefix) + prefix.Length).Trim();
            }
        }
        return string.Empty;
    }

    private static string FindLoserClan(string[] lines)
    {
        foreach (string line in lines)
        {
            if (line.Contains("из Железных волков"))
            {
                int start = line.IndexOf("из ") + 3;
                return line.Substring(start).Trim();
            }
        }
        return string.Empty;
    }

    private static int FindLoserPoints(string[] lines)
    {
        foreach (string line in lines)
        {
            if (line.Contains("получили утешительную награду:"))
            {
                string tail = line.Substring(line.IndexOf("награду: ") + 9);
                return int.Parse(tail.Split(' ')[0]);
            }
        }
        return 0;
    }

    private static int CountLevel(string[] lines, string level)
    {
        int count = 0;
        bool inEvent = false;
        string marker = "[" + level + "]";

        foreach (string line in lines)
        {
            if (line.Contains("Событие началось: Восстание Ледяного Пламени"))
                inEvent = true;

            if (inEvent && line.Contains(marker))
                count++;

            if (line.Contains("Событие \"Восстание Ледяного Пламени\" закрыто"))
                inEvent = false;
        }
        return count;
    }

    private static string FindTopPlayer(string[] lines)
    {
        string best = string.Empty;
        int max = -1;

        foreach (string line in lines)
        {
            if (line.Contains("Игрок ") && line.Contains(" победил ") && line.Contains(" врагов"))
            {
                string message = line.Substring(line.IndexOf("] ") + 2);
                string[] words = message.Split(' ');
                string name = words[1];
                int kills = int.Parse(words[3]);

                if (kills > max)
                {
                    max = kills;
                    best = $"{name} ({kills} побед)";
                }
            }
        }
        return best;
    }
}