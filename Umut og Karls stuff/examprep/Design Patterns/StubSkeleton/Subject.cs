namespace StubSkeleton;

public interface ISubject
{
    void PerformOperation();
}

public class Subject : ISubject
{
    public void PerformOperation()
    {
        Console.WriteLine("RealSubject is performing the operation.");
    }
}