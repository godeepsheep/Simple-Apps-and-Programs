using System.Net.Sockets;
using System.Text;

public class Client
{
    private int port;

    public Client(int port)
    {
        this.port = port;
    }

    public void Run(string input)
    {
        try
        {
            var client = new TcpClient("127.0.0.1", port);
            Console.WriteLine("Connected to server");

            var stream = client.GetStream();

            // Send beskeden til serveren
            byte[] dataToSend = Encoding.UTF8.GetBytes(input);
            stream.Write(dataToSend, 0, dataToSend.Length);
            Console.WriteLine($"Sendt til serveren: {input}");

            // Modtag respons fra serveren
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Modtaget fra serveren: {response}");

            // Luk forbindelsen
            stream.Close();
            client.Close();
            Console.WriteLine("Forbindelsen er lukket.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl: {ex.Message}");
        }
    }
}