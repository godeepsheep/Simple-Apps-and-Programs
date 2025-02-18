using System.Net.Sockets;
using System.Text;

public class Klient
{
    private int _port;

    public Klient(int port)
    {
        this._port = port;
    }

    public void Run()
    {
        try
        {
            var client = new TcpClient("127.0.0.1", _port);
            Console.WriteLine("Client connected");
            var stream = client.GetStream();
            var numbers = new[] { 15, 35, 5, 21, 22 };

            foreach (var number in numbers)
            {
                var message = number.ToString();
                var data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Thread.Sleep(50); // Small delay to ensure the server processes each message
            }

            // Read all responses (5 numbers in reversed order)
            var buffer = new byte[1024];
            var bytesRead = stream.Read(buffer, 0, buffer.Length);
            var response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // Split response by newline and process each number
            var responses = response.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            foreach (var res in responses)
            {
                Console.WriteLine($"Response: {res}");
            }

            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }
}
