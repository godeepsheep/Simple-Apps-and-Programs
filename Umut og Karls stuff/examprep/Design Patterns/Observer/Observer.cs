namespace Observer;

public interface IObserver
{
    void Update();
}

public class ConcreteObserver : IObserver
{
    private readonly ConcreteSubject _subject;

    public ConcreteObserver(ConcreteSubject subject)
    {
        _subject = subject;
        _subject.Attach(this);
    }

    public void Update()
    {
        Console.WriteLine($"The new state is: {_subject.State}");
    }
}