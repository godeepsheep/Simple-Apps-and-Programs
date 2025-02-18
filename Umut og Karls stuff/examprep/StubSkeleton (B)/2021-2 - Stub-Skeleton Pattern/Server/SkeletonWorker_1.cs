using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using StubSkeletonPattern.Common;

namespace StubSkeletonPattern.Server {

  internal class SkeletonWorker {
    private Thread thread;
    private bool stop;

    private readonly SkeletonManager manager;

    private readonly Socket connection;

    private StreamReader reader;
    private StreamWriter writer;

    internal SkeletonWorker(SkeletonManager manager, Socket connection) {
      this.manager = manager;
      this.connection = connection;

      stop = false;
    }

    internal void Start() {
      thread = new Thread(Run);
      thread.Start();
    }

    private void Run() {
      //IPEndPoint ipEndPoint = connection.RemoteEndPoint as IPEndPoint;
      // ... ipEndPoint.Address
      // ... ipEndPoint.Address.GetAddressBytes()[ 0 ], 0-3
      // ... ipEndPoint.Port

      WriteLine("New connection: " + connection.RemoteEndPoint);

      using (NetworkStream stream = new NetworkStream(connection) {
        ReadTimeout = 100
      })
        using (reader = new StreamReader(stream))
        using (writer = new StreamWriter(stream))
          try {
            string line = reader.ReadLine();

            WriteLine("Received: " + line);

            using (StringReader sr = new StringReader(line)) {
              XmlReaderSettings settings = new XmlReaderSettings {
                IgnoreComments = true,
                IgnoreWhitespace = true
              };

              using (XmlReader reader = XmlReader.Create(sr, settings)) {
                reader.MoveToContent();

                string tagName = reader.LocalName;

                switch (tagName) {
                  case "person":
                    HandlePerson(line);
                    break;

                  case "oldest":
                    writer.WriteLine(HandleOldest(line));
                    break;
                }
              }
            }
          }
          catch (IOException) {
            // ignorer
            Thread.Sleep(100); // nap!
          }

      manager.Remove(this);

      WriteLine("Terminated");
    }

    private string HandleOldest(string line) {
      Person p1, p2;

      /*
       * deserialize method call
       */
      using (StringReader sr = new StringReader(line)) {
        XmlReaderSettings settings = new XmlReaderSettings {
          IgnoreComments = true,
          IgnoreWhitespace = true
        };

        using (XmlReader reader = XmlReader.Create(sr, settings)) {
          reader.MoveToContent();

          reader.ReadStartElement("oldest");
          {
            p1 = new Person();
            p1.FromXml(reader);

            p2 = new Person();
            p2.FromXml(reader);
          }
          reader.ReadEndElement(); // </oldest>
        }
      }

      /*
       * call method
       */
      CalledObject calledObj = new CalledObject();

      Person result = calledObj.Oldest(p1, p2);

      return result.ToXml();
    }

    private void HandlePerson(string line) {
      Person p = new Person();
      p.FromXml(line);

      WriteLine(p.ToString());
    }

    protected string Tail(string text) {
      int pos = text.IndexOf(' ');

      if (pos >= 0)
        return text.Substring(pos + 1).Trim();
      else
        return string.Empty;
    }

    protected void SendLine(string line) {
      WriteLine("Sending line: " + line);

      writer.WriteLine(line);
      writer.Flush();

      Thread.Sleep(100); // nap, afht. simuleringen
    }

    public void Shutdown(bool waitForTermination=false) {
      WriteLine("Shutdown requested");

      stop = true;

      if (waitForTermination)
        thread.Join();
    }

    protected void WriteLine(string line) {
      ConsoleUtils.WriteLine(ConsoleColor.DarkGreen, "[ClientWorker] " + line);
    }
  }
}
