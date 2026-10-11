using System.Runtime.CompilerServices;
using System.Text;

namespace lab1;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();

        string filename = "server_log.txt";

        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        Console.WriteLine("Вас приветствует утилита для администратора игрового сервера.\nНа данный момент таков статус серверов:");

        string name = "server1";
        string ip_address = "104.16.132.229";
        int port = 27015;

        double min_ping = 4.0;
        double max_ping = 400.0;
        double ping = Math.Round((min_ping + (max_ping - min_ping) * random.NextDouble()), 2);
        double min_packet_loss = 0.01;
        double max_packet_loss = 7.0;
        double packet_loss = Math.Round((min_packet_loss + (max_packet_loss - min_packet_loss) * random.NextDouble()), 2);
        string server_status = "Online";

        int cpu = random.Next(10, 100);
        int ram = random.Next(10, 100);
        int hhd = 56;

        int players_online = random.Next(5, 118);
        int admins_online = random.Next(2, 10);

        Console.WriteLine($"\nСостояние: {server_status}");
        Console.WriteLine($"Название сервера: {name}");
        Console.WriteLine($"IP-адрес (IPv4): {ip_address}, Порт: {port}");

        Console.WriteLine($"\nПинг составляет: {ping} ms");
        Console.WriteLine($"Потеря пакетов: {packet_loss}%");

        Console.WriteLine($"\nCPU: {cpu}%");
        Console.WriteLine($"RAM: {ram}%");
        Console.WriteLine($"HHD: {hhd}%");

        Console.WriteLine($"\nИгроков онлайн: {players_online}/128");
        Console.WriteLine($"Админов онлайн: {admins_online}/10");

        Console.WriteLine("Сохранить логи текущего состояния? (y/n)");
        string ans = Console.ReadLine();
        if (ans == "y")
        {
            string[] lines =
            {
                $"LAST CHECK LOG:\n\nname = {name}",
                $"ip_address = {ip_address}",
                $"port = {port}",
                $"packet_loss = {packet_loss}",
                $"server_status = {server_status}",
                $"CPU = {cpu}",
                $"RAM = {ram}",
                $"HDD = {hhd}",
                $"players_online = {players_online}",
                $"admins_online = {admins_online}",
                
            };
            File.WriteAllLines(filename, lines, Encoding.UTF8);
        }
        
    }
}