using System.Net;
using System.Net.Sockets;
using System.Text;

//Get our IPs on our network with DNS
IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());

//Get our own IPadress - Index 0 is our own connection.
IPAddress ipAddress = ipHostEntry.AddressList[0];

//Instantiate our communication endpoint using IPAdress & portnumber as constructor params.
//Both our client and server will connect to the same endpoint to communicate.
var ipEndPoint = new IPEndPoint(ipAddress, 12345);

//Instantiate our socket
var socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

//Start server and accept incomming communication.
socket.Bind(ipEndPoint);
socket.Listen();

while (true) {
    Socket connection = socket.Accept(); //Blocks untill a client wants to connect.

    var thread = new Thread(async () => { //Takes delegate ThreadStart with characteristics void & no params.
        bool isFirstOdd = true;
        bool isFirstEven = true;
        while (true) {
            //Handle the server logic.
            //instantiate a buffer to store the communcation bytes.
            var messageBuffer = new byte[1_024];

            //wait for the client to communicate in bytes.
            int bytesReceived = await connection.ReceiveAsync(messageBuffer, SocketFlags.None);

            //Encode bytes to string
            string stringReceived = Encoding.UTF8.GetString(messageBuffer, 0, bytesReceived);

            //Break message to stop the server.
            if (stringReceived.Equals("eom")) {
                break;
            }

            string response;
            //A number with correct format is sent.
            try {
                //Parse string to int 
                int intReceived = Int32.Parse(stringReceived);

                //Handle the number & find response
                if (intReceived % 2 == 0) {
                    if (isFirstEven) {
                        response = "lige";
                        isFirstEven = false;
                    } else {
                        response = "igen lige";
                    }
                } else {
                    if (isFirstOdd) {
                        response = "ulige";
                        isFirstOdd = false;
                    } else {
                        response = "igen ulige";
                    }
                }

                //Send response to the client
                byte[] acknowledgementBytes = Encoding.UTF8.GetBytes(response);
                _ = connection.SendAsync(acknowledgementBytes, 0);

                //A number without correct format is sent.
            } catch (FormatException) {
                response = "ugyldigt tal modtaget!";

                //Send response to the client
                byte[] acknowledgementBytes = Encoding.UTF8.GetBytes(response);
                _ = connection.SendAsync(acknowledgementBytes, 0);
            }
        }
    });
    thread.Start();
}
