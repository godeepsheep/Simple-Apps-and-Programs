using StubSkeletonPattern.Client;
using StubSkeletonPattern.Server;
using System;
using System.Threading;

namespace StubSkeletonPattern {

  public class Program {

    public static void Main() {
      SkeletonManager manager = new SkeletonManager(6010);
      manager.Start();

      Console.WriteLine("Client started");

      Stub stub = new Stub("127.0.0.1", 6010);

      Person p = new Person("Jens Jensen", 30);

      Console.WriteLine("Sending person: " + p);

      stub.Send(p);

      Console.WriteLine("Person send");

      Thread.Sleep(3000);
      manager.Shutdown(false);
    }

    public static void TestXML() {
      Person p1 = new Person("Jens Jensen", 30);

      string xml = p1.ToXml();

      Console.WriteLine(xml);

      // skal vide at det er en Person

      Person p2 = new Person();

      p2.FromXml(xml);

      Console.WriteLine(p2);
    }
  }
}
