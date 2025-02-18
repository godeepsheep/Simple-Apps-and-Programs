class Program
{
    public static void Main(string[] args)
    {
        Server server = new Server("127.0.0.1", 8888);
        Client client = new Client("127.0.0.1", 8888);

        server.Start();
        client.Start();
    }
}