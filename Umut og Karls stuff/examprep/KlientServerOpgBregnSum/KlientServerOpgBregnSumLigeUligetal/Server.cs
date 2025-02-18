using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace KlientServerOpgBregnSumLigeUligetal
{
    public class Server
    {
        private int port;

        public Server(int port)
        {
            this.port = port;
        }

        public void Run()
        {
            try
            {
                var server = new TcpListener(IPAddress.Any, port);
                server.Start();
                Console.WriteLine("Server started");

                while (true)  // Serveren kører i en uendelig løkke for at håndtere flere klienter
                {
                    var client = server.AcceptTcpClient();
                    Console.WriteLine("Client connected");
                    var stream = client.GetStream();

                    try
                    {
                        while (true)
                        {
                            // Buffer til indgående data
                            byte[] buffer = new byte[1024]; // Opretter en byte array
                            int bytesRead = stream.Read(buffer, 0, buffer.Length); // Læser man data fra strøm

                            if (bytesRead == 0) break;  // Hvis klienten lukker forbindelsen

                            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                            Console.WriteLine($"Modtaget: {receivedData}");

                            // TODO: Implementer logik til at analysere tal og beregne summen

                            var numbers = receivedData.Split(',')
                                .Select(s => int.TryParse(s.Trim(), out int num) ? num : (int?)null)
                                .Where(n => n.HasValue)
                                .Select(n => n.Value)
                                .ToList();
                
                            var lige = numbers.Where(n => n % 2 == 0).Sum();
                            var ulige = numbers.Where(n => n % 2 != 0).Sum();
                            
                            var response = $"Sum af lige tal: {lige}, Sum af ulige tal: {ulige}";
                            
                            // Send en respons tilbage til klienten
                            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                            stream.Write(responseBytes, 0, responseBytes.Length);
                            Console.WriteLine("Respons sendt til klienten.");
                        }
                    }
                    finally
                    {
                        // Luk forbindelsen efter hver klient
                        stream.Close();
                        client.Close();
                        Console.WriteLine("Client disconnected");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}