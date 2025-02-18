using StubSkeletonPattern.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace StubSkeletonPattern.Client {

  internal class Stub {
    private string host;
    private int port;

    private StreamReader reader;
    private StreamWriter writer;

    public Stub(string host, int port) {
      this.host = host;
      this.port = port;
    }

    internal void Send(Person p) {
      try {
        using (Socket connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)) {
          connection.Connect(new IPEndPoint(IPAddress.Parse(host), port));

          using (NetworkStream stream = new NetworkStream(connection))
          using (reader = new StreamReader(stream))
          using (writer = new StreamWriter(stream)) {
            string xml = p.ToXml();

            WriteLine(xml);

            SendLine(xml);
          }
        }

        WriteLine("Terminated");
      }
      catch {
        WriteLine("Could not connect to server");
      }
    }

    private void SendLine(string line) {
      WriteLine("Sending line: " + line);

      writer.WriteLine(line);
      writer.Flush();

      Thread.Sleep(100); // nap, afht. simuleringen
    }

    private void WriteLine(string line) {
      ConsoleUtils.WriteLine(ConsoleColor.DarkGray, "[Stub] " + line);
    }
  }
}
