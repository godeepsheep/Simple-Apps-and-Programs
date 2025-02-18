using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Server
{
    static void Main(string[] args)
    {
        TcpListener server = null;
        try
        {
            int port = 12345; // Port number where the server listens for connections
            server = new TcpListener(IPAddress.Any, port); // Create a listener on all network interfaces
            server.Start(); // Start listening for incoming connections
            Console.WriteLine($"Server started on port {port}");

            while (true) // Keep the server running indefinitely
            {
                Console.WriteLine("Waiting for a connection...");
                TcpClient client = server.AcceptTcpClient(); // Accept a new client connection
                Console.WriteLine("Client connected!");

                // Handle the client in a new thread to allow multiple clients simultaneously
                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.Start();
            }
        }
        finally
        {
            server?.Stop(); // Stop the server when the program ends
        }
    }

    static void HandleClient(TcpClient client)
    {
        try
        {
            NetworkStream stream = client.GetStream(); // Get the network stream for communication
            byte[] buffer = new byte[1024]; // Buffer to hold incoming data
            int bytesRead;

            // Read and process data from the client until the connection is closed
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                // Convert the received data to a string
                string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {received}");

                // Echo the message back to the client
                byte[] response = Encoding.UTF8.GetBytes($"Server: {received}");
                stream.Write(response, 0, response.Length); // Send response to the client
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        finally
        {
            client.Close(); // Close the client connection
            Console.WriteLine("Client disconnected");
        }
    }
}
