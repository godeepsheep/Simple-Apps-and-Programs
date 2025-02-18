using System.Net.Sockets;

public class Klient
{
    public void Run()
    {
        try
        {
            var client = new TcpClient("127.0.0.1", 6010);
            Console.WriteLine("Client connected");

            var stream = client.GetStream();
            var writer = new StreamWriter(stream) { AutoFlush = true };

            sendline(writer, "fenerbahce fk Hello my name is umutcan and i like fenerbahce SK");

            writer.Close();
            client.Close(); // Lukker TCP-forbindelsen korrekt

            Console.WriteLine("Client disconnected");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void sendline(StreamWriter writer, string line)
    {
        Console.WriteLine("Sending line: " + line);

        writer.WriteLine(line); // Brug WriteLine i stedet for write
    }
}
