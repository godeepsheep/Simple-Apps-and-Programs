using demo_netværkclienttcp;

var port = 12345;

var server = new Server(port);
var serverThread = new Thread(server.Run);
serverThread.Start();

// Klient 1
var client1Thread = new Thread(() =>
{
    var client1 = new Client(port);
    client1.Run("33 44 11 22");
});
client1Thread.Start();

// Klient 2
var client2Thread = new Thread(() =>
{
    var client2 = new Client(port);
    client2.Run("10 20 30 40");
});
client2Thread.Start();