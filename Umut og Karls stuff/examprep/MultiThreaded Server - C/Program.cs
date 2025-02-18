using System.Net;
using System.Net.Sockets;
using System.Text;

//Get our networks connection to the DNS
IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());

//List of addresses connected to the network. 0 is our own.
IPAddress ipAddress = ipHostEntry.AddressList[0];

//Instantiate end point from our IP & Port number. Both the client and server will connect to the same endpoint to communicate.
var ipEndPoint = new IPEndPoint(ipAddress, 12345);
var socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


await socket.ConnectAsync(ipEndPoint);

while (true) {
    //Blocks until a message is entered in the console.
    string message = Console.ReadLine();

    if (message.Equals("eom")) {
        break;
    }

    //Convert to byte[] to send.
    byte[] messageBytes = Encoding.UTF8.GetBytes(message);

    _ = await socket.SendAsync(messageBytes, SocketFlags.None);

    //Receive message from the server
    var buffer = new byte[1_024];
    int bytesReceived = await socket.ReceiveAsync(buffer, SocketFlags.None);
    string converted = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
    Console.WriteLine($"Client received the message from the server: \"{converted}\"!");
}
