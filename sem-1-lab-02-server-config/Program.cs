namespace sem_1_lab_02_server_config;

public class Program
{
    public static string CheckConfiguration(int players, int minRAM, bool publicServer, bool passwordServer)
    {
        if (players <= 0)
            return "Запуск невозможен: количество игроков должно быть больше нуля.";

        if (minRAM < 2)
            return "Запуск невозможен: серверу недостаточно оперативной памяти.";

        if (players > minRAM * 20)
            return "Запуск возможен с предупреждением: для такого количества игроков рекомендуется больше оперативной памяти.";

        if (passwordServer)
            return "Запуск возможен с предупреждением: публичный сервер защищён паролем.";

        if (publicServer == false)
            return "Запуск возможен с предупреждением: сервер не является публичным (доступ возможен только по прямому подключению)";

        return "Сервер готов к запуску.";
    }
    static void Main(string[] args)
    {
        Console.WriteLine("------------------------------ Рутинная проверка работоспособности заданной конфигурации сервера ------------------------------");
        Console.WriteLine($"\n Автоматическая проверка показала: {CheckConfiguration(128, 8, true, false)}\n\n" + new string('-', 127));
    }
}