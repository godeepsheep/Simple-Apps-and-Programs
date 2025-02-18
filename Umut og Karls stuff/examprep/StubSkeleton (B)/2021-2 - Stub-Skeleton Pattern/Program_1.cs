using StubSkeletonPattern.Client;
using StubSkeletonPattern.Server;
using System;
using System.Threading;

namespace StubSkeletonPattern {

  public class Program {

    public static void Main() {
      TestOldest();
    }

    // opgave C+D
    public static void TestOldest() {
      /*
        * machine B
        */
      SkeletonManager manager = new SkeletonManager(6010);
      manager.Start();

      /*
       * machine A
       */
      Stub stub = new Stub("127.0.0.1", 6010);

      Person p1 = new Person("Jens Jensen", 30);
      Person p2 = new Person("Ole Olesen", 25);

      Person p3 = stub.Oldest(p1, p2);

      Console.WriteLine("[Main] Oldest person: " + p3);

      /*
       * machine B
       */
      Thread.Sleep(3000);
      manager.Shutdown(false);
    }

    // opgave B
    public static void TestPerson() {
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

    // opgave A
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
