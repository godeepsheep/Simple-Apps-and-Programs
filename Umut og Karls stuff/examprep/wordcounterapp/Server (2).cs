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
                    int letters = Countletters(line);
                    string word = GetFirstWord(line);
                    Console.WriteLine("Modtaget: " + line);
                    Console.WriteLine("count of letters: "+letters + " first word in the line is: " + word);

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
        private int Countletters(string text)
        {
            return text.Count(char.IsLetter);
        }
        private string GetFirstWord(string text)
        {
            var candidate = text.Trim();
            if (!candidate.Any(Char.IsWhiteSpace))
                return text;

            return candidate.Split(' ').FirstOrDefault();
        }
    }
}