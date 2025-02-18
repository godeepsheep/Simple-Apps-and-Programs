using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    int port;

    private bool running = true;
    private TcpListener server;
    private NetworkStream stream;

    public Server(string host, int port)
    {
        this.port = port;
    }

    public void Start()
    {
        Thread thread = new Thread(new ThreadStart(Run));
        thread.Start();
    }

    public void Run()
    {
        server = new TcpListener(IPAddress.Any, port);
        server.Start();

        Console.WriteLine("Server started. Waiting for connections...");

        TcpClient client = server.AcceptTcpClient();
        stream = client.GetStream();

        CommandSkeleton[] commands = new CommandSkeleton[]{
            new CommandSkeleton("Reverse", "Reverse a string", "str"),
            new CommandSkeleton("Concat", "Concatenate strings", "...str"),
        };

        while (running)
        {
            string message = Receive();
            if (message == "PING")
            {
                Send(XMLWriter.ToXML(commands));
            }
            else if (message.StartsWith("<"))
            {
                CommandStub command = XMLReader.StubFromXML(message);
                Send("= " + Handler.Execute(command.Method, command.Arguments));
                Thread.Sleep(6000);
                Send(XMLWriter.ToXML(commands));
            }
        }

        stream.Close();
        client.Close();
        server.Stop();
    }

    public string Receive() {
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer);
        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        if (bytesRead > 0)
        {
            Console.WriteLine("[Server] Received: " + message);
        }
        return message;
    }

    public void Send(string message)
    {
        Console.WriteLine("[Server] Sending: " + message);
        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
        stream.Flush();
    }
}