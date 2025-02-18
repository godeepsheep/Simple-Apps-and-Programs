using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PrøveEksamenOpgaveNetværk
{
    public class Server
    {
        private int port;
        private HashSet<int> _receivedNumbers; 

        public Server(int port)
        {
            this.port = port;
            _receivedNumbers = new HashSet<int>(); // Initialiser HashSet
        }

        public void Run()
        {
            TcpListener server = null;

            try
            {
                server = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
                server.Start();
                Console.WriteLine("Server started og venter på klient...");

                while (true)
                {
                    var client = server.AcceptTcpClient();
                    Console.WriteLine("Klient tilsluttet!");

                    var stream = client.GetStream();

                    // Fortsæt med at lytte efter beskeder fra klienten, indtil forbindelsen lukkes
                    while (true)
                    {
                        var buffer = new byte[1024];
                        var bytesRead = stream.Read(buffer, 0, buffer.Length);

                        // Hvis bytesRead er 0, betyder det, at klienten har lukket forbindelsen
                        if (bytesRead == 0)
                        {
                            break;
                        }

                        var clientMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Modtaget fra klient: {clientMessage}");
                        
                        var number = int.Parse(clientMessage); // Konvertere besked til et tal
                        var responseMessage = HandleNumber(number); // Bestem svar på tallet

                        // Send svar tilbage til klienten
                        var responseData = Encoding.UTF8.GetBytes(responseMessage); // Opret svar
                        stream.Write(responseData, 0, responseData.Length);
                    }

                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
            }
            finally
            {
                server?.Stop();
            }
        }

        // Metode til at bestemme om tallet er lige eller ulige
        private string GetParity(int number)
        {
            return number % 2 == 0 ? "Lige" : "Ulige";
        }

        // Metode til at håndtere tal, hvis de er modtaget før eller ej
        private string HandleNumber(int number)
        {
            if (_receivedNumbers.Contains(number))
            {
                return "Igen " + GetParity(number); // Hvis tallet er set før, returner "Igen"
            }
            else
            {
                _receivedNumbers.Add(number); // Tilføj tallet til HashSet
                return GetParity(number); // Første gang tallet modtages, returner "Lige" eller "Ulige"
            }
        }
    }
}
