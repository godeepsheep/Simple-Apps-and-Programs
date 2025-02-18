
namespace StubSkeletonPattern.Server {

  internal class CalledObject {

    internal Person Oldest(Person p1, Person p2) {
      if (p1.CompareTo(p2) >= 0)
        return p1;
      else
        return p2;
    }
  }
}
