using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace BeregnSumAfArrayTalNetværksprogrammering
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

                while (true) // Serveren lytter konstant efter nye klienter
                {
                    var tcpClient = tcpListener.AcceptTcpClient();
                    Console.WriteLine("Klient forbundet!");

                    var stream = tcpClient.GetStream();
                    var reader = new StreamReader(stream);
                    var writer = new StreamWriter(stream) { AutoFlush = true };

                    string line;
                    while ((line = reader.ReadLine()) != null) // Læser linje for linje fra klienten
                    {
                        //Console.WriteLine($"Modtaget: {line}");

                        var digits = ConvertToArray(line);
                        Array.Reverse(digits); // Vend rækkefølgen af tallene
                        var sum = CalculateSum(digits);

                        Console.WriteLine($"Omvendt array: {string.Join(", ", digits)} Sum: {sum}");
                    }

                    reader.Close();
                    tcpClient.Close(); // Lukker forbindelsen korrekt
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
            }
        }

        private int[] ConvertToArray(string line)
        {
            return line.Split(' ') // Splitter strengen ved mellemrum
                       .Select(int.Parse) // Konverterer hver del til et heltal
                       .ToArray(); // Laver det til et array
        }

        private int CalculateSum(int[] numbers)
        {
            return numbers.Sum(); // Beregner summen af alle tal
        }
    }
}
