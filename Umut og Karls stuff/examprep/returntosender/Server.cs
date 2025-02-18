using System.Net.Sockets;
using System.Net;
using System.Text;
namespace returntosender;
public class Server
{
    private int _port;

    public Server(int port)
    {
        this._port = port;
    }

    public void Run()
    {
        try
        {
            var server = new TcpListener(IPAddress.Parse("127.0.0.1"), _port);
            server.Start();
            Console.WriteLine("Server started");

            while (true)
            {
                var client = server.AcceptTcpClient();
                Console.WriteLine("Client connected");

                var stream = client.GetStream();
                var numbersReceived = new List<int>();

                // Receive exactly 5 numbers from the client
                while (numbersReceived.Count < 5)
                {
                    var buffer = new byte[1024];
                    var bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    var clientMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();
                    Console.WriteLine($"Received: {clientMessage}");

                    if (int.TryParse(clientMessage, out var number))
                    {
                        numbersReceived.Add(number);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid input: {clientMessage}");
                    }
                }

                // Calculate the sum of all numbers
                var sum = CalculateSum(numbersReceived);
                Console.WriteLine($"Sum of received numbers: {sum}");

                // Reverse the numbers
                numbersReceived.Reverse();

                // Send each reversed number back to the client
                foreach (var number in numbersReceived)
                {
                    var responseData = Encoding.UTF8.GetBytes(number + "\n"); // Send each number with a newline delimiter
                    stream.Write(responseData, 0, responseData.Length);
                    Console.WriteLine($"Sent back: {number}");
                }

                client.Close(); // Close connection after processing
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }

    // Method to calculate the sum of all numbers
    private int CalculateSum(List<int> numbers)
    {
        return numbers.Sum();
    }
}

