using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;

class Client
{
    private string host;
    private int port;

    private bool running = true;
    private TcpClient client;
    private NetworkStream stream;

    public Client(string host, int port)
    {
        this.host = host;
        this.port = port;
    }

    public void Start()
    {
        Thread thread = new Thread(new ThreadStart(Run));
        thread.Start();
    }

    public void Run()
    {
        client = new TcpClient(host, port);
        stream = client.GetStream();

        Send("PING");
        while (running)
        {
            string message = Receive();

            if (message.StartsWith("="))
            {
                Console.WriteLine("\nRESULT " + message);
            }
            else if (message.StartsWith("<"))
            {
                Console.Clear();
                List<CommandSkeleton> commands = XMLReader.SkeletonsFromXML(message);

                if (commands.Count > 0)
                {
                    Console.WriteLine("\nCommand List:");
                    for (int i = 0; i < commands.Count; i++)
                    {
                        CommandSkeleton command = commands[i];
                        Console.WriteLine($"{i + 1}. {command.Method} [{String.Join(",", command.Parameters)}]");
                        Console.WriteLine($"  {command.Description}");
                        Console.WriteLine("");
                    }

                    string input = "";
                    while (input == "")
                    {
                        Console.Write($"Choose Command [1-{commands.Count}]: ");
                        input = Console.ReadLine() ?? "";

                        int choice = 0;
                        try
                        {
                            choice = int.Parse(input);
                            if (choice < 1 || choice > commands.Count)
                            {
                                throw new Exception("Number is out of range");
                            }
                        }
                        catch
                        {
                            input = "";
                            continue;
                        }

                        List<string> arguments = new();
                        if (commands[choice - 1].Parameters[0].StartsWith("..."))
                        {
                            while (true)
                            {
                                Console.Write("Arg: ");
                                input = Console.ReadLine() ?? "";

                                if (input.Trim() == "")
                                {
                                    Console.WriteLine("");
                                    break;
                                }
                                else
                                {
                                    arguments.Add(input);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < commands[choice - 1].Parameters.Count; i++)
                            {
                                Console.Write($"Arg {i + 1} ({commands[choice - 1].Parameters[i]}): ");
                                arguments.Add(Console.ReadLine() ?? "");
                            }
                        }

                        Console.WriteLine("");

                        CommandStub command = new CommandStub(commands[choice - 1].Method, arguments);
                        Send(XMLWriter.ToXML(command));
                        break;
                    }
                }
                else
                {
                    Send("PING");
                }
            }
        }

        stream.Close();
    }

    public string Receive() {
        byte[] buffer = new byte[1024];
        int bytesRead = stream.ReadAtLeast(buffer, 2, false);
        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        if (bytesRead > 0)
        {
            Console.WriteLine("[Client] Received: " + message);
        }
        return message;
    }

    public void Send(string message)
    {
        Console.WriteLine("[Client] Sending: " + message);
        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
        stream.Flush();
    }
}