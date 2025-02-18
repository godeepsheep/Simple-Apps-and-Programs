using System;
using System.Net.Sockets;
using System.Text;

namespace PrøveEksamenOpgaveNetværk
{
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
                using var client = new TcpClient("127.0.0.1", port);
                Console.WriteLine("Forbundet til serveren!");

                var stream = client.GetStream();

                // Send beskeder til serveren
                var numbers = new[] { 82, 19, 43, 2, 61, 19 };
                foreach (var number in numbers)
                {
                    var message = number.ToString();
                    var dataToSend = Encoding.UTF8.GetBytes(message); // Konverter til bytes
                    stream.Write(dataToSend, 0, dataToSend.Length); // Send til serveren

                    // Læs svar fra serveren
                    var buffer = new byte[1024];
                    var bytesRead = stream.Read(buffer, 0, buffer.Length);
                    var serverResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Server responded: {serverResponse}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
            }
        }
    }
}