using System;
using System.Threading;
using MultiThreaded_Server.Client;
using MultiThreaded_Server.Common;
using MultiThreaded_Server.Server;

namespace MultiThreaded_Server {

  public class Program {

    private static void Main() {
      int port = 6010;

      ClientManager manager = new ClientManager(port);
      manager.Start();

      Thread.Sleep(100); // venter på at server er online

      RunScriptedClient(
        "127.0.0.1", port,

        "LOGIN viggo",
        "MESSAGE niels Hej, hvordan går det?",
        "LOGOUT"
      );

      Thread.Sleep(2000); // venter på client terminerer

      RunScriptedClient(
        "127.0.0.1", port,

        "LOGIN niels",
        "GET",
        "GET",
        "LOGOUT"
      );

      Thread.Sleep(5000);
      manager.Shutdown(true);

      ConsoleUtils.WriteLine(ConsoleColor.Red, "[Main] Terminated");
    }

    // som udgangspunkt vil flere clients køre samtidig!
    private static void RunScriptedClient(string host, int port, params string[] lines) {
      ScriptedClient client = new ScriptedClient(host, port, lines);
      client.Start();
    }
  }
}
