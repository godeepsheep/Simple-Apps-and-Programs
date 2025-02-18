using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main(string[] args)
    {
        try
        {
            string serverIp = "127.0.0.1"; // The server's IP address (localhost in this case)
            int port = 12345; // Port number to connect to

            TcpClient client = new TcpClient(serverIp, port); // Connect to the server
            Console.WriteLine("Connected to the server!");

            NetworkStream stream = client.GetStream(); // Get the network stream for communication

            while (true) // Loop for sending messages to the server
            {
                Console.Write("Enter a message: ");
                string message = Console.ReadLine(); // Get user input

                if (string.IsNullOrEmpty(message)) // Exit if the user presses Enter without typing
                    break;

                // Convert the message to a byte array and send it to the server
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // Receive and display the server's response
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine(response); // Print the server's response
            }

            client.Close(); // Close the connection to the server
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}
