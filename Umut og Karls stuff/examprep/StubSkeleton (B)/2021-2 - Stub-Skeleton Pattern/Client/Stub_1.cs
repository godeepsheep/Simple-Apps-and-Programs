using StubSkeletonPattern.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml;

namespace StubSkeletonPattern.Client {

  internal class Stub {
    private string host;
    private int port;

    private StreamReader sr;
    private StreamWriter sw;

    public Stub(string host, int port) {
      this.host = host;
      this.port = port;
    }

    internal void Send(Person p) {
      try {
        using (Socket connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)) {
          connection.Connect(new IPEndPoint(IPAddress.Parse(host), port));

          using (NetworkStream stream = new NetworkStream(connection))
          using (sr = new StreamReader(stream))
          using (sw = new StreamWriter(stream)) {
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

    internal Person Oldest(Person p1, Person p2) {
      try {
        using (Socket connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)) {
          connection.Connect(new IPEndPoint(IPAddress.Parse(host), port));

          using (NetworkStream stream = new NetworkStream(connection))
          using (sr = new StreamReader(stream))
          using (sw = new StreamWriter(stream)) {
            /*
             * call method
             */
            using (StringWriter stringWriter = new StringWriter()) {
              using (XmlWriter writer = XmlWriter.Create(stringWriter)) {
                writer.WriteStartDocument();
                {
                  writer.WriteStartElement("oldest");
                  {
                    p1.ToXml(writer);
                    p2.ToXml(writer);
                  }
                  writer.WriteEndElement(); // </oldest>
                }
                writer.WriteEndDocument();
              }

              // send method call as xml
              SendLine(stringWriter.ToString());
            }

            /*
             * return value
             */
            string resultStr = sr.ReadLine();

            Person oldestPerson = new Person();
            oldestPerson.FromXml(resultStr);

            return oldestPerson;
          }
        }
      }
      catch {
        WriteLine("Could not connect to server");

        return null;
      }
    }

    private void SendLine(string line) {
      WriteLine("Sending line: " + line);

      sw.WriteLine(line);
      sw.Flush();

      Thread.Sleep(100); // nap, afht. simuleringen
    }

    private void WriteLine(string line) {
      ConsoleUtils.WriteLine(ConsoleColor.DarkGray, "[Stub] " + line);
    }
  }
}
