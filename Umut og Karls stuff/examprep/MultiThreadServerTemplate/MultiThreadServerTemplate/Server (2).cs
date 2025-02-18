using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace MultiThreadServerTemplate
{
    public class Server
    {
        public void Run()
        {
            try
            {
                var tcpListener = new TcpListener(IPAddress.Any, 6010);
                tcpListener.Start();
                Console.WriteLine("Server started");

                var tcpClient = tcpListener.AcceptTcpClient();
                var stream = tcpClient.GetStream();
                var reader = new StreamReader(stream);

                string line;
                while ((line = reader.ReadLine()) != null) // Læser linje for linje fra klienten
                {
                    Console.WriteLine("Modtaget: " + line);
                }

                reader.Close();
                tcpClient.Close(); // Lukker TCP-forbindelsen korrekt

                Console.WriteLine("Server stopped");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}