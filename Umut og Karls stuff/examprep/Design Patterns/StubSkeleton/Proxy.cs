namespace StubSkeleton;

// Proxy (Stub/Skeleton)
public class Proxy : ISubject
{
    private readonly Subject _subject;

    public Proxy()
    {
        _subject = new Subject();
    }

    public void PerformOperation()
    {
        Console.WriteLine("Proxy is performing pre-operation tasks.");
        _subject.PerformOperation();
        Console.WriteLine("Proxy is performing post-operation tasks.");
    }
}