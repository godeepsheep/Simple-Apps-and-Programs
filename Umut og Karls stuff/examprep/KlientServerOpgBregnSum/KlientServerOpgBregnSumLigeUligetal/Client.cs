using System;
using System.Net.Sockets;
using System.Text;

public class Client
{
    private int port;

    public Client(int port)
    {
        this.port = port;
    }

    public void Run()
    {
        try
        {
            var client = new TcpClient("127.0.0.1", port);
            Console.WriteLine("Connected to server");

            var stream = client.GetStream();

                var message = "1, 2, 3, 4";

                // Send beskeden til serveren
                byte[] dataToSend = Encoding.UTF8.GetBytes(message);
                stream.Write(dataToSend, 0, dataToSend.Length);

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